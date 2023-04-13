﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.AspNetCore
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 设置文件下载名称
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="fileName"></param>
        public static void FileInlineHandle(this HttpContext httpContext, string fileName)
        {
            string encodeFilename = System.Web.HttpUtility.UrlEncode(fileName, Encoding.GetEncoding("UTF-8"));
            httpContext.Response.Headers.Add("Content-Disposition", "inline;filename=" + encodeFilename);

        }

        /// <summary>
        /// 设置文件附件名称
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="fileName"></param>
        public static void FileAttachmentHandle(this HttpContext httpContext, string fileName)
        {
            string encodeFilename = System.Web.HttpUtility.UrlEncode(fileName, Encoding.GetEncoding("UTF-8"));
            httpContext.Response.Headers.Add("Content-Disposition", "attachment;filename=" + encodeFilename);

        }

        /// <summary>
        /// 获取语言种类
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetLanguage(this HttpContext httpContext)
        {
            string res = "zh-CN";
            var str = httpContext.Request.Headers["Accept-Language"].FirstOrDefault();
            if (str is not null)
            {
                res = str.Split(",")[0];
            }
            return res;

        }

        /// <summary>
        /// 判断是否为异步请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIp(this HttpContext context)
        {
            if (context == null) return "";
            var result = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(result))
            {
                result = context.Connection.RemoteIpAddress?.ToString();
            }
            if (string.IsNullOrEmpty(result) || result.Contains("::1"))
                result = "127.0.0.1";

            result = result.Replace("::ffff:", "127.0.0.1");

            //Ip规则效验
            var regResult = Regex.IsMatch(result, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");

            result = regResult ? result : "127.0.0.1";
            return result;
        }

        /// <summary>
        /// 获取浏览器标识
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserAgent(this HttpContext context)
        {
            return context.Request.Headers["User-Agent"];
        }
    }
}
