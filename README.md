# Unity 游戏项目合集

包含两个独立的 Unity 小游戏项目。

---

## 1. 3DRPG — 第三人称动作 RPG

基于**有限状态机 (FSM)** 的 3D 角色控制演示。

### 系统架构

| 系统 | 说明 |
|------|------|
| 状态机 | `StateBase` 抽象基类，通过 `ChangeState<T>()` 泛型切换状态。所有状态 (Idle/Run/Jump/Fly/Attack/Damage/Die/Interactive) 作为 MonoBehaviour 挂载在玩家对象上 |
| 输入 | `InputManager` 封装 Unity Input System，提供 Attack1/Attack2/Jump/Interactive/Move/Camera 输入 |
| 摄像机 | 三种摄像机脚本：`CameraFollow`（跟随）、`CameraControl`（水平旋转）、`CameraChat`（轨道环绕+LookAt） |
| 背包 | `ItemData` (ScriptableObject) 定义物品，`Inventory` 管理物品列表，`ItemPickup` 实现世界空间拾取 |
| UI | 主菜单（开始/退出）、拾取提示、画布自适应缩放 |

### 角色状态流转

```
Idle ──→ Run (移动输入) ──→ Idle (松手)
  │
  ├──→ Jump ──→ Fly (空中再按跳跃) ──→ Idle (落地)
  │     └──→ Idle (0.6s 后落地)
  ├──→ Attack1/2/3/4 (攻击键) ──→ Idle (动画结束)
  ├──→ Interactive (交互键) ──→ Idle
  ├──→ Damage (受击) ──→ Idle
  └──→ Die (死亡) ──→ Idle (5s 后复活)
```

---

## 2. Remove — 三消纸牌游戏

点击消除类 2D 小游戏，三层叠放卡牌，每次消除需要 3 张相同的牌。

### 核心机制

- **棋盘生成**：6×4 网格（可配置），每层有 XY 偏移，共 3 层，排序层级递增 (0-99, 100-199, 200-299)
- **遮挡判定**：通过 `Physics2D.OverlapBoxAll` 检测，排序层级更高的牌会遮挡下层牌（遮挡牌变黑不可点击）
- **消除规则**：点击未遮挡的牌放入卡牌栏，当末尾 3 张相同时自动消除；卡牌栏超过 7 张则游戏失败
- **洗牌算法**：Fisher-Yates 洗牌，每 3 张为一组，要求宽或高能被 3 整除

### 文件说明

| 文件 | 职责 |
|------|------|
| `GameManage.cs` | 主逻辑：生成棋盘、处理点击、刷新遮挡状态 |
| `ItemControl.cs` | 单张卡牌：加载精灵、检测遮挡、响应点击 |
| `CardManager.cs` | 卡牌栏：管理已选卡牌、检测三消、判断失败 |
| `GameUIManager.cs` | 游戏内 UI：刷新、设置、返回菜单 |
| `UIManager.cs` | 主菜单：开始游戏、退出 |

---

## 运行要求

- Unity 2021.3+
- TextMesh Pro（Remove 项目需要）
- Unity Input System Package（3DRPG 项目需要）
