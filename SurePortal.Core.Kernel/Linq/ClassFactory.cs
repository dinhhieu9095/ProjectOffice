using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace DaiPhatDat.Core.Kernel.Linq
{
    public class ClassFactory
    {
        public static readonly ClassFactory Instance = new ClassFactory();
        private int classCount;
        private readonly Dictionary<Signature, TypeInfo> classes;
        private readonly ModuleBuilder module;

        private ClassFactory()
        {
            var assemblyBuilder =
                AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("DynamicClasses"), AssemblyBuilderAccess.Run);
            module = assemblyBuilder.DefineDynamicModule("Module");
            classes = new Dictionary<Signature, TypeInfo>();
        }

        public TypeInfo GetDynamicClass(IEnumerable<DynamicProperty> properties)
        {
            var key = new Signature(properties);
            TypeInfo dynamicClass;
            if (!classes.TryGetValue(key, out dynamicClass))
            {
                dynamicClass = CreateDynamicClass(key.properties);
                classes.Add(key, dynamicClass);
            }

            return dynamicClass;
        }

        private TypeInfo CreateDynamicClass(DynamicProperty[] properties)
        {
            var name = "DynamicClass" + (classCount + 1);
            var tb = module.DefineType(name, TypeAttributes.Public, typeof(DynamicClass));
            var properties1 = GenerateProperties(tb, properties);
            GenerateEquals(tb, properties1);
            GenerateGetHashCode(tb, properties1);
            var typeInfo = tb.CreateTypeInfo();
            ++classCount;
            return typeInfo;
        }

        private void GenerateEquals(TypeBuilder tb, FieldInfo[] fields)
        {
            var ilGenerator = tb.DefineMethod("Equals",
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(bool),
                new Type[1]
                {
                    typeof(object)
                }).GetILGenerator();
            var local = ilGenerator.DeclareLocal(tb.BaseType);
            var label1 = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Isinst, tb.BaseType);
            ilGenerator.Emit(OpCodes.Stloc, local);
            ilGenerator.Emit(OpCodes.Ldloc, local);
            ilGenerator.Emit(OpCodes.Brtrue_S, label1);
            ilGenerator.Emit(OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Ret);
            ilGenerator.MarkLabel(label1);
            foreach (var field in fields)
            {
                var fieldType = field.FieldType;
                var type = typeof(EqualityComparer<>).MakeGenericType(fieldType);
                var label2 = ilGenerator.DefineLabel();
                ilGenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), null);
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldfld, field);
                ilGenerator.Emit(OpCodes.Ldloc, local);
                ilGenerator.Emit(OpCodes.Ldfld, field);
                ilGenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("Equals", new Type[2]
                {
                    fieldType,
                    fieldType
                }), null);
                ilGenerator.Emit(OpCodes.Brtrue_S, label2);
                ilGenerator.Emit(OpCodes.Ldc_I4_0);
                ilGenerator.Emit(OpCodes.Ret);
                ilGenerator.MarkLabel(label2);
            }

            ilGenerator.Emit(OpCodes.Ldc_I4_1);
            ilGenerator.Emit(OpCodes.Ret);
        }

        private void GenerateGetHashCode(TypeBuilder tb, FieldInfo[] fields)
        {
            var ilGenerator = tb.DefineMethod("GetHashCode",
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(int),
                Type.EmptyTypes).GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldc_I4_0);
            foreach (var field in fields)
            {
                var fieldType = field.FieldType;
                var type = typeof(EqualityComparer<>).MakeGenericType(fieldType);
                ilGenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), null);
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldfld, field);
                ilGenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("GetHashCode", new Type[1]
                {
                    fieldType
                }), null);
                ilGenerator.Emit(OpCodes.Xor);
            }

            ilGenerator.Emit(OpCodes.Ret);
        }

        private FieldInfo[] GenerateProperties(TypeBuilder tb, DynamicProperty[] properties)
        {
            var fieldInfoArray = (FieldInfo[])new FieldBuilder[properties.Length];
            for (var index = 0; index < properties.Length; ++index)
            {
                var property = properties[index];
                var fieldBuilder = tb.DefineField("_" + property.Name, property.Type, FieldAttributes.Private);
                var propertyBuilder =
                    tb.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.Type, null);
                var mdBuilder1 = tb.DefineMethod("get_" + property.Name,
                    MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, property.Type,
                    Type.EmptyTypes);
                var ilGenerator1 = mdBuilder1.GetILGenerator();
                ilGenerator1.Emit(OpCodes.Ldarg_0);
                ilGenerator1.Emit(OpCodes.Ldfld, fieldBuilder);
                ilGenerator1.Emit(OpCodes.Ret);
                var mdBuilder2 = tb.DefineMethod("set_" + property.Name,
                    MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, null,
                    new Type[1]
                    {
                        property.Type
                    });
                var ilGenerator2 = mdBuilder2.GetILGenerator();
                ilGenerator2.Emit(OpCodes.Ldarg_0);
                ilGenerator2.Emit(OpCodes.Ldarg_1);
                ilGenerator2.Emit(OpCodes.Stfld, fieldBuilder);
                ilGenerator2.Emit(OpCodes.Ret);
                propertyBuilder.SetGetMethod(mdBuilder1);
                propertyBuilder.SetSetMethod(mdBuilder2);
                fieldInfoArray[index] = fieldBuilder;
            }

            return fieldInfoArray;
        }
    }
}