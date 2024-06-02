﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;
using Yi.Abp.Tool.Application.Contracts;
using Yi.Abp.Tool.Application.Contracts.Dtos;
using Yi.Abp.Tool.Domain;
using Yi.Abp.Tool.Domain.Shared.Dtos;
using Yi.Framework.Core.Helper;

namespace Yi.Abp.Tool.Application
{
    public class TemplateGenService : IRemoteService, ITemplateGenService, ITransientDependency
    {
        private readonly TemplateGenManager _templateGenManager;
        public TemplateGenService(TemplateGenManager templateGenManager) { _templateGenManager = templateGenManager; }

        /// <summary>
        /// 下载模块文件
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateModuleAsync(TemplateGenCreateInputDto moduleCreateInputDto)
        {
            moduleCreateInputDto.SetNameReplace();

            var input = moduleCreateInputDto.Adapt<TemplateGenCreateDto>();
            input.SetTemplateFilePath(_templateGenManager._toolOptions.ModuleTemplateFilePath);
            var filePath = await _templateGenManager.CreateTemplateAsync(input);

            //考虑从路径中获取
            var fileContentType = MimeHelper.GetMimeMapping(Path.GetFileName(filePath));
            //设置附件下载，下载名称
            return new FileContentResult(await File.ReadAllBytesAsync(filePath), fileContentType);
        }

        /// <summary>
        /// 下载模块文件
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateProjectAsync(TemplateGenCreateInputDto moduleCreateInputDto)
        {
            moduleCreateInputDto.SetNameReplace();

            var input = moduleCreateInputDto.Adapt<TemplateGenCreateDto>();
            input.SetTemplateFilePath(_templateGenManager._toolOptions.ProjectTemplateFilePath);
            var filePath = await _templateGenManager.CreateTemplateAsync(input);

            //考虑从路径中获取
            var fileContentType = MimeHelper.GetMimeMapping(Path.GetFileName(filePath));
            //设置附件下载，下载名称
            return new FileContentResult(await File.ReadAllBytesAsync(filePath), fileContentType);
        }
    }
}
