using System;
using System.Collections.Generic;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Bbs.Consts;
using Yi.Furion.Core.Bbs.Enums;
using Yi.Furion.Core.Rbac.Dtos.User;

namespace Yi.Furion.Core.Bbs.Dtos.Discuss
{
    public class DiscussGetListOutputDto : IEntityDto<long>
    {
        /// <summary>
        /// �Ƿ��ѵ���
        /// </summary>
        public bool IsAgree { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Types { get; set; }
        public string? Introduction { get; set; }

        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }

        //������ѯ���������ݣ����ܿ���
        //public string Content { get; set; }
        public string? Color { get; set; }

        public long PlateId { get; set; }

        //�Ƿ��ö���Ĭ��false
        public bool IsTop { get; set; }

        public DiscussPermissionTypeEnum PermissionType { get; set; }
        //�Ƿ��ֹ��Ĭ��false
        public bool IsBan { get; set; }


        /// <summary>
        /// ����
        /// </summary>
        public string? Cover { get; set; }

        //˽����Ҫ�ж�codeȨ��
        public string? PrivateCode { get; set; }
        public DateTime CreationTime { get; set; }

        public List<long> PermissionUserIds { get; set; }

        public UserGetListOutputDto User { get; set; }

        public void SetBan()
        {
            this.Title = DiscussConst.Privacy;
            this.Introduction = "";
            this.Cover = null;
            //����ֹ
            this.IsBan = true;
        }
    }


    public static class DiscussGetListOutputDtoExtension
    {

        public static void ApplyPermissionTypeFilter(this List<DiscussGetListOutputDto> dtos, long userId)
        {
            dtos?.ForEach(dto =>
            {
                switch (dto.PermissionType)
                {
                    case DiscussPermissionTypeEnum.Public:
                        break;
                    case DiscussPermissionTypeEnum.Oneself:
                        //��ǰ�����ǽ��Լ��ɼ���ͬʱ���ǵ�ǰ��¼�û�
                        if (dto.User.Id != userId)
                        {
                            dto.SetBan();
                        }
                        break;
                    case DiscussPermissionTypeEnum.User:
                        //��ǰ����Ϊ���ֿɼ���ͬʱ���ǵ�ǰ��¼�û� Ҳ ���ڿɼ��û��б���
                        if (dto.User.Id != userId && !dto.PermissionUserIds.Contains(userId))
                        {
                            dto.SetBan();
                        }
                        break;
                    default:
                        break;
                }
            });
        }

    }

}
