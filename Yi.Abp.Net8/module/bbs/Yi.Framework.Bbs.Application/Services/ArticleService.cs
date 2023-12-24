using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Article;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Plate;
using Yi.Framework.Bbs.Application.Contracts.IServices;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Bbs.Domain.Extensions;
using Yi.Framework.Bbs.Domain.Repositories;
using Yi.Framework.Bbs.Domain.Shared.Consts;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Domain.Authorization;
using Yi.Framework.Rbac.Domain.Shared.Consts;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Bbs.Application.Services
{
    /// <summary>
    /// Article服务实现
    /// </summary>

    public class ArticleService : YiCrudAppService<ArticleEntity, ArticleGetOutputDto, ArticleGetListOutputDto, Guid, ArticleGetListInputVo, ArticleCreateInputVo, ArticleUpdateInputVo>,
       IArticleService
    {
        public ArticleService(IArticleRepository articleRepository,
            ISqlSugarRepository<DiscussEntity> discussRepository,
            IDiscussService discussService) : base(articleRepository)
        {

            _articleRepository = articleRepository;
            _discussRepository = discussRepository;
            _discussService = discussService;


        }
        private IArticleRepository _articleRepository { get; set; }
        private ISqlSugarRepository<DiscussEntity> _discussRepository { get; set; }
        private IDiscussService _discussService { get; set; }

        public override async Task<PagedResultDto<ArticleGetListOutputDto>> GetListAsync(ArticleGetListInputVo input)
        {
            RefAsync<int> total = 0;

            var entities = await _articleRepository._DbQueryable.WhereIF(!string.IsNullOrEmpty(input.Name), x => x.Name.Contains(input.Name!))
                          //.WhereIF(!string.IsNullOrEmpty(input.Code), x => x.Name.Contains(input.Code!))
                          .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                          .ToPageListAsync(input.SkipCount, input.MaxResultCount, total);
            return new PagedResultDto<ArticleGetListOutputDto>(total, await MapToGetListOutputDtosAsync(entities));
        }


        /// <summary>
        /// 获取文章全部树级信息
        /// </summary>
        /// <param name="discussId"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [Route("article/all/discuss-id/{discussId}")]
        public async Task<List<ArticleAllOutputDto>> GetAllAsync([FromRoute] Guid discussId)
        {
            await _discussService.VerifyDiscussPermissionAsync(discussId);


            var entities = await _articleRepository.GetTreeAsync(x => x.DiscussId == discussId);
            //var result = entities.Tile();
            var items = entities.Adapt<List<ArticleAllOutputDto>>();
            return items;
        }

        /// <summary>
        /// 查询文章
        /// </summary>
        /// <param name="discussId"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<List<ArticleGetListOutputDto>> GetDiscussIdAsync([FromRoute] Guid discussId)
        {
            if (!await _discussRepository.IsAnyAsync(x => x.Id == discussId))
            {
                throw new UserFriendlyException(DiscussConst.No_Exist);
            }

            var entities = await _articleRepository.GetTreeAsync(x => x.DiscussId == discussId);
            var items = await MapToGetListOutputDtosAsync(entities);
            return items;
        }

        /// <summary>
        /// 发表文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [Permission("bbs:article:add")]
        [Authorize]
        public async override Task<ArticleGetOutputDto> CreateAsync(ArticleCreateInputVo input)
        {
            await VerifyDiscussCreateIdAsync(input.DiscussId);
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<ArticleGetOutputDto> UpdateAsync(Guid id, ArticleUpdateInputVo input)
        {
            var entity = await _articleRepository.GetByIdAsync(id);
            await VerifyDiscussCreateIdAsync(entity.DiscussId);
            return await base.UpdateAsync(id, input);
        }


        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task DeleteAsync(Guid id)
        {
            var entity = await _articleRepository.GetByIdAsync(id);
            await VerifyDiscussCreateIdAsync(entity.DiscussId);
            await base.DeleteAsync(id);
        }




        /// <summary>
        /// 效验创建权限，userId为主题创建者
        /// </summary>
        /// <param name="disucssId"></param>
        /// <returns></returns>
        private async Task VerifyDiscussCreateIdAsync(Guid disucssId)
        {
            var discuss = await _discussRepository.GetFirstAsync(x => x.Id == disucssId);
            if (discuss is null)
            {
                throw new UserFriendlyException(DiscussConst.No_Exist);
            }

            //这块有点绕，这个版本的写法比较清晰
            bool result = false;

            if (this.CurrentUser.GetPermissions().Contains(UserConst.AdminPermissionCode))
            {
                //如果是超管,直接跳过
                result = true;
            }
            else
            {
                //如果不是超管,必须满足作者是自己，同时还有发布的权限
                if (discuss.CreatorId == CurrentUser.Id)
                {
                    result = true;
                }
            }

            if (!result)
            {
                throw new UserFriendlyException("权限不足，请联系主题作者或管理员申请开通");
            }
        }
    }
}
