//using FluentNHibernate.Utils;
//using System;
//using System.Data;
//using System.Data.Common;
//using System.Data.Entity.Infrastructure.Interception;
//using System.Linq;
//using System.Text.RegularExpressions;

//namespace SurePortal.Core.Kernel.AmbientScope
//{
//    public class FullTextInterceptor : IDbCommandInterceptor
//    {
//        private const string Prefix = "-FULLTEXTSPREFIX-";

//        /// <inheritdoc />
//        /// <summary>
//        /// </summary>
//        /// <param name="command"></param>
//        /// <param name="interceptionContext"></param>
//        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
//        {
//        }

//        /// <inheritdoc />
//        /// <summary>
//        /// </summary>
//        /// <param name="command"></param>
//        /// <param name="interceptionContext"></param>
//        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
//        {
//        }

//        /// <inheritdoc />
//        /// <summary>
//        /// </summary>
//        /// <param name="command"></param>
//        /// <param name="interceptionContext"></param>
//        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
//        {
//            Query(command);
//        }

//        /// <inheritdoc />
//        /// <summary>
//        /// </summary>
//        /// <param name="command"></param>
//        /// <param name="interceptionContext"></param>
//        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
//        {
//        }

//        /// <inheritdoc />
//        /// <summary>
//        /// </summary>
//        /// <param name="command"></param>
//        /// <param name="interceptionContext"></param>
//        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
//        {
//            Query(command);
//        }

//        /// <inheritdoc />
//        /// <summary>
//        /// </summary>
//        /// <param name="command"></param>
//        /// <param name="interceptionContext"></param>
//        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
//        {
//        }

//        /// <summary>
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public static string Value(string value)
//        {
//            var parts = Regex
//                .Matches(value
//                    , @"[""].+?[""]|[^ ]+")
//                .Cast<Match>()
//                .Select(match =>
//                {
//                    if (match.Value.StartsWith("\"")
//                        && match.Value.EndsWith("\""))
//                        return match.Value;

//                    return match.Value.Replace("\"", string.Empty);
//                })
//                .ToList();

//            var fullTextValue = string.Empty;

//            if (parts.Count > 1)
//                for (var i = 0; i < parts.Count; i++)
//                {
//                    var values = Regex
//                        .Split(parts[i], @"\s{1,}")
//                        .Select(text =>
//                        {
//                            if (text.StartsWith("\"")
//                                && text.EndsWith("\""))
//                                return text;

//                            return text.Replace("\"", string.Empty);
//                        })
//                        .ToList();

//                    if (values.Count > 1)
//                        fullTextValue += i == 0
//                            ? $"NEAR({string.Join(",", values)})"
//                            : $" OR NEAR({string.Join(",", values)})";
//                    else
//                        fullTextValue += i == 0
//                            ? values[0]
//                            : $" OR {values[0]}";
//                }
//            else
//                fullTextValue = value;

//            return $"{Prefix}{fullTextValue}";
//        }

//        /// <summary>
//        /// </summary>
//        /// <param name="command"></param>
//        private static void Query(DbCommand command)
//        {
//            var text = command.CommandText;

//            for (var i = 0; i < command.Parameters.Count; i++)
//            {
//                var parameter = command.Parameters[i];

//                if (!parameter.DbType.In(DbType.String
//                    , DbType.AnsiString
//                    , DbType.StringFixedLength
//                    , DbType.AnsiStringFixedLength))
//                    continue;

//                if (parameter.Value == DBNull.Value) continue;

//                if (!(parameter.Value is string value)
//                    || value.IndexOf(Prefix, StringComparison.Ordinal) < 0)
//                    continue;

//                value = value.Replace(Prefix, string.Empty);
//                value = value.Substring(1, value.Length - 2);

//                parameter.Value = value;

//                command.CommandText = Regex.Replace(text
//                    , $@"\[(\w*)\].\[(\w*)\]\s*LIKE\s*@{parameter.ParameterName}\s?(?:ESCAPE N?'~')"
//                    , $@"CONTAINS([$1].[$2], @{parameter.ParameterName})");

//                if (text == command.CommandText)
//                    throw new InvalidOperationException($"Prefix was not replaced on: {text}");

//                text = command.CommandText;
//            }
//        }
//    }
//}