﻿using SurePortal.Core.Kernel.Caches;
using SurePortal.Core.Kernel.Domain.ValueObjects;
using SurePortal.Core.Kernel.Resources.Application.Dto;
using System;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Web;

namespace SurePortal.Core.Kernel
{
    public class ResourceManagement
    {
        #region Contructors

        protected ResourceManagement() { }

        #endregion

        #region Function
        public static string GetResourceText(string resourceID)
        {
            return resourceID;
            //if (App.DicResources != null && App.DicResources.ContainsKey(resourceID))
            //{
            //    #region Get Language

            //    string LANGUAGECODE = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims
            //        .Where(x => x.Type == ClaimTypes.Country)
            //        .Select(x => x.Value)
            //        ?.FirstOrDefault();
            //    LANGUAGECODE = string.IsNullOrEmpty(LANGUAGECODE) ? Constant.Language_Vietnamese : LANGUAGECODE;

            //    #endregion

            //    #region Get UserId

            //    string USERID = ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims
            //        .Where(x => x.Type == ClaimTypes.Name)
            //        .Select(x => x.Value)
            //        ?.FirstOrDefault();
            //    USERID = string.IsNullOrEmpty(USERID) ? Constant.Anonymous : USERID;

            //    #endregion

            //    var langCode = App.CacheProvider.GetLocalLanguage(USERID, LANGUAGECODE);
            //    if (langCode == SurePortalLanguages.Vietnamese && !string.IsNullOrEmpty(App.DicResources[resourceID].ResourceText0))
            //    {
            //        return App.DicResources[resourceID].ResourceText0;
            //    }
            //    if (langCode == SurePortalLanguages.English && !string.IsNullOrEmpty(App.DicResources[resourceID].ResourceText1))
            //    {
            //        return App.DicResources[resourceID].ResourceText1;
            //    }
            //    if (langCode == SurePortalLanguages.French && !string.IsNullOrEmpty(App.DicResources[resourceID].ResourceText2))
            //    {
            //        return App.DicResources[resourceID].ResourceText2;
            //    }
            //    if (langCode == SurePortalLanguages.Chinese && !string.IsNullOrEmpty(App.DicResources[resourceID].ResourceText3))
            //    {
            //        return App.DicResources[resourceID].ResourceText3;
            //    }
            //    if (langCode == SurePortalLanguages.Japanese && !string.IsNullOrEmpty(App.DicResources[resourceID].ResourceText4))
            //    {
            //        return App.DicResources[resourceID].ResourceText4;
            //    }
            //    if (langCode == SurePortalLanguages.Russian && !string.IsNullOrEmpty(App.DicResources[resourceID].ResourceText5))
            //    {
            //        return App.DicResources[resourceID].ResourceText5;
            //    }
            //}
            //return !string.IsNullOrEmpty(App.DicResources[resourceID].ResourceText0)
            //    ? App.DicResources[resourceID].ResourceText0
            //    : resourceID.Split('.')[resourceID.Split('.').Length - 1];
        }
        public static string GetResourceText(string resourceID, string defaultTextVie)
        {
            if (string.IsNullOrEmpty(resourceID))
            {
                return Error_GetResourceID;
            }
            else
            {
                //if (App.DicResources == null)
                //{
                //    return defaultTextVie;
                //}
                //else if (App.DicResources != null && App.DicResources.ContainsKey(resourceID))
                //{
                //    string mResource = GetResourceText(resourceID);
                //    if (string.IsNullOrEmpty(mResource)) { return defaultTextVie; }
                //    if (mResource != resourceID)
                //    {
                //        return mResource;
                //    }
                //    //else
                //    //{
                //    //    if (string.IsNullOrEmpty(App.DicResources[resourceID].ResourceText0))
                //    //    {
                //    //        App.DicResources[resourceID].ResourceText0 = defaultTextVie;
                //    //        ResourceDto res = new ResourceDto();
                //    //        res.ResourceID = resourceID;
                //    //        res.DefaultText0 = defaultTextVie;
                //    //        res.ResourceText0 = defaultTextVie;
                //    //        var service = Autofac.GetService<IResourceBusiness>();
                //    //        service.UpdateAsync(res);
                //    //    }
                //    //}
                //}
                //else
                //{
                //    ResourceDto res = new ResourceDto();
                //    res.ResourceID = resourceID;
                //    res.DefaultText0 = defaultTextVie;
                //    res.ResourceText0 = defaultTextVie;
                //    //var service = new GetService<ResourceServices>();
                //    //var result = service.Insert(res);
                //    //if (result)
                //    //{
                //    //    App.DicResources.Remove(resourceID);
                //    //    App.DicResources.Add(resourceID, res);
                //    //}
                //}
                return defaultTextVie;
            }
        }
        public static string GetResourceText(string resourceID, string defaultTextVie, string defaultTextEng)
        {
            if (string.IsNullOrEmpty(resourceID))
            {
                return Error_GetResourceID;
            }
            else
            {
                //if (App.DicResources == null)
                //{
                //    return defaultTextVie;
                //}
                //else if (App.DicResources != null && App.DicResources.ContainsKey(resourceID))
                //{
                //    #region Update

                //    string mResource = GetResourceText(resourceID);
                //    if (string.IsNullOrEmpty(mResource))
                //    {
                //        return defaultTextVie;
                //    }
                //    if (mResource != resourceID)
                //    {
                //        return mResource;
                //    }
                //    //else
                //    //{
                //    //    if (string.IsNullOrEmpty(App.DicResources[resourceID].ResourceText0))
                //    //    {
                //    //        App.DicResources[resourceID].ResourceText0 = defaultTextVie;
                //    //        App.DicResources[resourceID].ResourceText1 = defaultTextEng;
                //    //        ResourceDto res = new ResourceDto();
                //    //        res.ResourceID = resourceID;
                //    //        res.DefaultText0 = defaultTextVie;
                //    //        res.ResourceText0 = defaultTextVie;
                //    //        res.DefaultText1 = defaultTextEng;
                //    //        res.ResourceText1 = defaultTextEng;
                //    //        var service = Autofac.GetService<IResourceBusiness>();
                //    //        service.UpdateAsync(res);
                //    //    }
                //    //}

                //    #endregion
                //}
                //else
                //{
                //    ResourceDto res = new ResourceDto();
                //    res.ResourceID = resourceID;
                //    res.DefaultText0 = defaultTextVie;
                //    res.ResourceText0 = defaultTextVie;
                //    res.DefaultText1 = defaultTextEng;
                //    res.ResourceText1 = defaultTextEng;
                //    //var service = Autofac.GetService<IResourceBusiness>();
                //    //bool result = service.Insert(res);
                //    //if (result)
                //    //{
                //    //    App.DicResources.Remove(resourceID);
                //    //    App.DicResources.Add(resourceID, res);
                //    //}
                //}
                return defaultTextVie;
            }
        }
        public static string GetResourceID(object classObject, string fieldName)
        {
            Type type = classObject.GetType();
            return GetResourceID(type, fieldName);
        }
        public static string GetResourceID(Type type, string fieldName)
        {
            return type.Name + "." + fieldName.Replace(" ", "-").Trim();
        }

        #endregion

        #region Cache
        
        /// <summary>
        /// Get ResourceID error!.
        /// </summary>
        public const string Error_GetResourceID = "Get ResourceID error!.";
        /// <summary>
        /// anonymous
        /// </summary>
        public const string Anonymous = "anonymous";

        #endregion
    }
    public class ResourceCache : Cache
    {
        public bool SetLocalLanguage(SurePortalLanguages languageCode, string userId)
        {
            string key = (Cache_LocalLanguage + userId).ToUpper();
            if (MemoryCache.Default.Contains(key))
            {
                MemoryCache.Default.Set(key, languageCode, DateTimeOffset.MaxValue);
            }
            else
            {
                MemoryCache.Default.Add(key, languageCode, DateTimeOffset.MaxValue);
            }
            return true;
        }
        public SurePortalLanguages GetLocalLanguage(string userId, string languageCode)
        {
            try
            {
                string key = (Cache_LocalLanguage + userId).ToUpper();
                object item = MemoryCache.Default.Get(key);
                if (item == null)
                {
                    return (languageCode == Language_Vietnamese) ? SurePortalLanguages.Vietnamese : SurePortalLanguages.English;
                }
                else
                {
                    return (SurePortalLanguages)item;
                }
            }
            catch
            {
                return SurePortalLanguages.Vietnamese;
            }
        }
        /// <summary>
        /// LocalLanguage
        /// </summary>
        public const string Cache_LocalLanguage = "LocalLanguage";
        /// <summary>
        /// vi-vn
        /// </summary>
        public const string Language_Vietnamese = "vi-vn";
        /// <summary>
        /// en-us
        /// </summary>
        public const string Language_English = "en-us";
        /// <summary>
        /// fr-fr
        /// </summary>
        public const string Language_French = "fr-fr";
        /// <summary>
        /// zh-cn
        /// </summary>
        public const string Language_Chinese = "zh-cn";
        /// <summary>
        /// ja-jp
        /// </summary>
        public const string Language_Japanese = "ja-jp";
        /// <summary>
        /// ru-ru
        /// </summary>
        public const string Language_Russian = "ru-ru";
    }
}