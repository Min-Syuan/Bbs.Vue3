using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Domain.Shared.Forum.ConstClasses;
using Yi.BBS.Domain.Shared.Forum.EnumClasses;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Application.Contracts.Identity.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos.Discuss
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



        public UserGetListOutputDto User { get; set; }
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
                            dto.Title = DiscussConst.˽��;
                            dto.Introduction= "";
                            dto.Cover = null;
                            //����ֹ
                            dto.IsBan = true;
                        }
                        break;
                    case DiscussPermissionTypeEnum.User:
                        break;
                    default:
                        break;
                }


            });
        }

    }

}
