
 
<h1 align="center"><img align="left" height="100px" src="https://user-images.githubusercontent.com/68722157/138828506-f58b7c57-5e10-4178-8f7d-5d5e12050113.png"> Yi框架</h1>
<h4 align="center">一套与SqlSugar一样爽的.Net6低代码开源框架</h4>
<h2 align="center">集大成者，终究轮子</h2>

[English](README-en.md) | 简体中文

![sdk](https://img.shields.io/badge/sdk-6.0.1-d.svg)![License MIT](https://img.shields.io/badge/license-Apache-blue.svg?style=flat-square)

****
### 简介:
**中文：意框架**（和他的名字一样“简易”）

**英文：YiFramework**

Yi框架-一套与SqlSugar一样爽的.Net6低代码开源框架。
与Sqlsugar理念一致，以用户体验出发。
架构干净整洁、无业务代码、采用微软风格原生框架封装、WebFrist开发。
适合.Net6学习、Sqlsugar学习 、项目二次开发。
集大成者，终究轮子

**分支**：

（本项目由EFCore版本历经3年不断迭代至Sqlsugar版本，现EFcore版本已弃用，目前sqlsugar不带任何业务，之后会更新业务功能）

**SqlSugar**:.Net6 DDD领域驱动设计 简单分层微服务架构

**ec**:EFcore完整电商项目

****

### 演示地址：

废话少说直接上地址，**请不要**更改里面的数据

API服务：~~[yi.ccnetcore.com](http://yi.ccnetcore.com)    管理员账号：admin 、 123~~

网关地址：~~[gate.ccnetcore.com/swagger](http://gate.ccnetcore.com/swagger)~~

WebFirst开发：所有代码生成器已经配置完成，无需任何操作数据库及任何代码，只需要网页表格上点点点即可

[https://www.donet5.com/Doc/11](https://www.donet5.com/Doc/11)

谁能把持的住Sqlsugar作者自己都依赖成瘾的东西呢？这是继DbFirst、CodeFirst下一代的划时代产品！无脑爽！

![image](https://s1.ax1x.com/2022/04/12/Lnm5Yq.png)

（首次添加实体后，生成代码记得修改对应的路径哦~~）

### 支持:

- [x] 完全支持单体应用架构
- [x] 完全支持分布式应用架构
- [x] 完全支持微服务架构
- [ ] 即将支持网格服务架构（我们将在后续版本加入dapr）

****
### 软件架构:

**架构**：后端.NET6(Asp.NetCore 6)、WebFirst代码生成器~~与.NET5(Asp.NetCore 5)、前端Vue（2.0）~~

**关系型数据库**：mysql、sql server、sqlite、oracle(正在兼容中)

**操作系统**：Windows、Linux

**身份验证**：JWT、IdentityServer4

**组件**：~~EFcore~~SqlSugar、Autofac、Castle、Swagger、Log4Net、Redis、RabbitMq、ES、Quartz.net、~~T4~~

**分布式**：CAP、Lock

**微服务**：Consul、Ocelot、IdentityService、Apollo、Docker、Jenkins、Nginx、K8s、ELK、Polly

**封装**：Json处理模块，滑动验证码模块，base64图片处理模块，异常捕捉模块、邮件处理模块、linq封装模块、随机数模块、统一接口模块、基于策略的jwt验证、过滤器、数据库连接、跨域、初始化种子数据、Base32、Console输出、日期处理、文件传输、html筛选、http请求、ip过滤、md5加密、Rsa加密、序列化、雪花算法、字符串处理、编码处理、地址处理、xml处理、心跳检查。。。

****
### 支持模块:

大致如图：

![image](https://user-images.githubusercontent.com/68722157/142923071-2fa524eb-e833-4143-a926-51566e56e889.png)
![image](https://user-images.githubusercontent.com/68722157/142923150-ebe1b538-c3fc-42dd-bea8-83e10e0f819a.png)
![image](https://user-images.githubusercontent.com/68722157/142923529-e4fbb2f6-def1-4702-b9da-5adbd22f0a2f.png)

(删除线代表已实现功能还未迁移过来)
- [x] 支持大致`DDD领域驱动设计`进行分层，支持微服务扩展
- [x] 支持采用`异步`开发awit/async
- [x] 支持数据库主从`读写分离`
- [x] 支持功能替换，无需改动代码，只需配置`json文件`进行装配即可
- [x] ~~-支持采用DbFirst开发方式，使用`T4模板代码生成器`，自动映射模型一键生成Service及IService所有代码~~
- [x] 支持WebFirst，无需改动代码，自动生成全套代码与数据库，只需点点点
- [x] ~~-支持`用户-角色-菜单-接口`以及vue2.0前端全部逻辑代码，下载无需修改直接使用~~
- [x] 支持`Aop封装`，FilterAop、IocAop、LogAop、SqlAop
- [x] 支持`Log4Net日志`记录，自动生成至bin目录下的logs文件夹
- [x] 支持`DbSeed数据库种子数据`接入
- [x] 支持主流`数据库随意切换`，Mysql/Sqlite/Sqlserver/Oracle
- [x] 支持上海杰哥官方`SqlSugar ORM`封装
- [x] 支持新版`SwaggerWebAPI`，jwt身份认证接入
- [x] 支持`Cors`跨域
- [x] 支持`AutoFac`自动映射依赖注入
- [x] 支持`consul`服务器注册与发现
- [x] 支持`健康检查`
- [x] 支持`RabbitMQ`消息队列
- [x] 支持`Redis`多级缓存 
- [x] 支持`Ocelot`网关，路由、服务聚合、服务发现、认证、鉴权、限流、熔断、缓存、Header头传递
- [x] 支持`Apollo`全局配置中心;
- [x] 支持`docker`镜像制作
- [x] ~~-支持页面`静态化处理`，将动态页面生成静态页面~~
- [x] 支持`Quartz.net`任务调度，实现任意接口被调度
- [x] 支持`ELK`，log4net+kafka+es+logstach+kibana
- [x] 支持`IdentityService4`授权中心
- [x] 支持`Es`分词查询
- [x] 支持多级`缓存`
- [x] 支持`CAP`分布式事务，mysql+rabbitMq
- [x] 支持`Docker+k8s`部署
- [x] 支持`Jenkins+CI/CD`
- [x] 支持`AutoMapper`模块映射
- [ ] 支持`微信支付`（没账号）
- [x] 支持`单表多租户`常用功能
- [x] 支持`逻辑删除`常用功能
- [x] 支持`操作日志`常用功能
- [x] 支持 太多了忘了

****
### 目录结构:

![图片](https://s1.ax1x.com/2022/04/09/LCTleH.png)

我们大致依照DDD领域驱动设计分层

什么？感觉太复杂了？用户只需关注Api、Service其他都是轮子啊！

~~- BackGround：后台进程（目前可以无视，等待更新）~~
- Client：客户端（测试、客户端）
- Domain：领域层（Dto、服务接口层、模型层、仓储层、服务层）
- Infrastructure：基础实例层(通用工具层、核心层、定时任务Job、国际化、Web扩展层)
- MicroServiceInstance：服务层（微服务）

****
### 安装教程:

我们将在之后更新教程手册！

1.  下载全部源码，默认使用sqlite数据库，已经生成
2.  直接点击sln文件运行即可，没有其他依赖

****
### 使用说明:

1.  导入使用仓库中的WebFirst数据库
2.  使用WebFirst添加实体、同步实体、修改模板生成路径并生成方案

 没了，恭喜你已经成功完成了项目，并且已经具备大部分通用场景业务
是不是一个字？爽！
到此为止，你无需写任何一个代码！
我们将使用说明转移至我们的官方论坛中，正在制作中，尽情期待！

****
### 感谢：

**大力支持**： Eleven神、Sqlsugar上海杰哥、Gerry、哲学的老张

[橙子]https://ccnetcore.com

[lzw]https://github.com/yeslode

[朝夕教育]https://www.zhaoxiedu.net

[Sqlsugar]https://www.donet5.com/Home/Doc

[RuYiAdmin]https://gitee.com/pang-mingjun/RuYiAdmin

[ZrAdminNetCore]https://gitee.com/izory/ZrAdminNetCore

****
### 联系我们：

作者QQ：454313500

联系作者，这里人人都是顾问

官方网址：正在建设

****
### FQA:

问1：为什么不采用EFcore？

答1：别问，问就是Sqlsugar，和本框架一样爽！

问2：以后会持续更新下去吗？

答2：一定会的，我们的标题是 一个和Sqlsugar一样爽的.Net6开源框架 ，只要Sqlsugar在，我们将一直更新下去。

问3：这个框架的针对人群是哪些人？适合所有人吗？

答3：并不是适合所有人，应该算适合需要有一定基础的开发人员，当然，如果你是大神，你完全可以将这个框架二次开发！

问4：花如此多的精力制作这个框架，是为了什么？是为了赚钱吗？和目前主流的abp等框架比，又有什么意义呢？

答4：我们与Sqlsugar作者理念一致，我们是从用户角度出发，框架是为用户服务，与ABP复杂上手理念完全是相反的。

问5：为何不出版一个详细的说明书呢？

答5：暂时不会了，之后可能会，代码都是基于Asp.NetCore框架，适用于新手不用造轮子，整个框架较为简单，阅读源码后，基本能自定义改造使用了，过难也已经封装完毕，别忘了，其意义是为了开发更加简易！建议添加作者好友，这里人人都是顾问。

我大抵要厌倦了负重前行。
