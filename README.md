# Unity Games

两个 Unity 小游戏项目。

---

## 3DRPG — 3D 角色扮演游戏

第三人称 3D 动作 RPG 原型，基于 Unity 输入系统和状态机架构。

### 功能

- **状态机系统** — 玩家行为通过状态模式管理：Idle（待机）、Run（移动）、Jump（跳跃）、Attack1~4（四段连击）、Damage（受击）、Die（死亡）、Fly（飞行）、Interactive（交互）
- **角色控制** — 移动、跳跃、连击攻击、交互，使用 Unity Input System 统一管理键盘/手柄输入
- **摄像机系统** — 第三人称跟随摄像机（CameraFollow），支持鼠标旋转（CameraControl），以及对话特写摄像机（CameraChat）
- **敌人系统** — 敌人受击和攻击逻辑
- **背包系统** — 物品数据（ItemData）、拾取（ItemPickup）、背包存储/容量限制（Inventory）
- **交互系统** — InteractiveBase 提供可交互物体基类
- **UI** — 血条、背包界面、拾取提示、分辨率设置、主菜单（开始/退出 + 异步加载进度条）

### 目录结构

```
3DRPG/Script/
├── PlayerControl.cs          # 玩家属性（血量、攻击力）
├── InputManager.cs           # 输入管理（移动/攻击/跳跃/交互/摄像机）
├── CameraControl.cs          # 摄像机旋转控制
├── CameraFollow.cs           # 第三人称跟随摄像机
├── CameraChat.cs             # 对话摄像机
├── EnemyControl.cs           # 敌人控制
├── InteractiveBase.cs        # 交互物体基类
├── InteractiveTest.cs        # 交互测试
├── IsGrounded.cs / IsGroundControl.cs  # 地面检测
├── FootStepEvent.cs          # 脚步音效事件
├── State/                    # 状态机
│   ├── StateBase.cs          # 状态基类（切换/受击）
│   ├── IdleState.cs / RunState.cs / JumpState.cs / FlyState.cs
│   ├── Attack1~4State.cs     # 四段连击
│   ├── DamageState.cs / DieState.cs
│   ├── InteractiveState.cs
│   └── AnimationEventReceiver.cs  # 动画事件接收
├── Item/                     # 背包系统
│   ├── Inventory.cs          # 背包存储
│   ├── ItemData.cs           # 物品数据
│   ├── ItemPickup.cs         # 物品拾取
│   └── ItemRotator.cs        # 物品旋转展示
└── UI/
    ├── UIManager.cs          # 主菜单
    ├── HpBar.cs              # 血条
    ├── InventoryUI.cs        # 背包界面
    ├── PickupUI.cs           # 拾取提示
    ├── PlayerPanelControl.cs # 玩家面板
    └── Resolution.cs         # 分辨率设置
```

---

## remove — 消除类休闲游戏

点击消除玩法：点击未被遮挡的卡片将其收集到卡槽，集齐 3 张相同卡片自动消除。

### 功能

- **三层卡牌布局** — 游戏生成 3 层交叠的卡牌，每层有微小的位置偏移
- **遮挡检测** — 上层卡牌遮挡下层卡牌，被遮挡的卡牌变黑且不可点击（Physics2D.OverlapBoxAll 检测）
- **三消机制** — 点击卡片加入底部卡槽，满 3 张相同卡片自动消除
- **失败判定** — 卡槽超过 7 张卡片时游戏失败
- **洗牌算法** — 确保每种卡片数量为 3 的倍数，可正常通关
- **主菜单** — 开始游戏 / 退出游戏

### 目录结构

```
remove/Script/
├── GameManage.cs             # 游戏主逻辑（生成卡牌/洗牌/点击检测）
├── CardManager.cs            # 卡槽管理（收集/三消/失败判定）
├── CardControl.cs            # 卡槽中的卡片显示
├── ItemControl.cs            # 场景中的卡片（加载图片/遮挡检测/点击）
├── GameUIManager.cs          # 游戏内 UI
├── UIManager.cs              # 主菜单
└── Resolution.cs             # 分辨率设置
```
