# Cooking

一款基于 Unity 开发的 2D 烹饪经营游戏。

## 项目简介

《Cooking》是一款 2D 烹饪经营类游戏：玩家经营一家餐厅，通过接待顾客、完成订单获取金币，并在四季与昼夜的时间推进中逐步扩张。

当前已完成客户端基础框架与主要 UI 模块，核心经营玩法正在开发中。

## 技术栈

| 项目 | 内容 |
| --- | --- |
| 引擎 | Unity 2022.3.62f3 LTS |
| 语言 | C# |
| 异步 | UniTask |
| UI | UGUI + TextMeshPro + SuperScrollView |
| 数据 | LitJson / JsonUtility |

## 已实现功能

**框架层**
- 分层架构：Model / Manager / Controller / UI 四层，依赖单向、职责清晰
- 事件系统：基于委托字典的观察者模式，支持多参数事件与运行时签名校验
- UI 框架：栈式面板管理，统一生命周期（OnOpen / OnClose / OnPause / OnResume）
- 组件自动绑定：UIBinder 按命名约定扫描并缓存 UI 组件，配套自定义 Inspector 一键刷新
- 存档系统：JSON 序列化，实现「玩家存档优先 → 内置配置回退 → 默认值兜底」的读取链路
- 多语言：中英文运行时切换，词条配置表 + 语言偏好持久化
- 音频管理：BGM / 环境音 / 音效三通道分离，音效播放源对象池化复用

**界面层**
- 开始界面：开始游戏、继续游戏、窗外集、设置、退出
- 设置界面：通用（音量 / 分辨率 / 语言 / 文本速度）与其他分页，设置项由 JSON 配置表驱动，扩展无需修改代码
- 游戏主界面：框架已接入，玩法内容开发中

## 目录结构

```
.
├── Cooking/                  Unity 工程目录
│   ├── Assets/
│   │   ├── Scenes/           场景
│   │   ├── Scripts/          脚本
│   │   │   ├── Controller/   控制层
│   │   │   ├── Manager/      管理器（UI / 音频 / 事件 / 数据 / 多语言）
│   │   │   ├── Model/        数据模型
│   │   │   ├── UI/           UI 面板
│   │   │   └── Extension/    第三方库（LitJson / SuperScrollView）
│   │   ├── Resources/        动态加载资源（预制体 / 图片 / 音频）
│   │   └── StreamingAssets/  内置配置与多语言文件
│   ├── Packages/             包依赖
│   └── ProjectSettings/      工程设置
└── README.md
```

## 运行方式

1. 使用 Unity **2022.3.62f3** 打开 `Cooking` 目录
2. 打开场景 `Assets/Scenes/SampleScene.unity`
3. 点击运行

## 开发进度

- [x] 客户端基础框架（分层架构 / 事件系统 / UI 框架）
- [x] 开始界面、设置界面
- [x] 多语言、存档、音频模块
- [ ] 核心经营玩法（接待顾客、订单、烹饪）
- [ ] 背包与食材系统
- [ ] 图鉴系统
