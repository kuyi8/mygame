# Unity Game Projects

两个 Unity 游戏项目：**3D RPG 动作游戏** 和 **2D 卡牌消除游戏**。

---

## 项目一：3DRPG — 第三人称动作 RPG

一个基于 Unity 的 3D 第三人称动作 RPG 游戏，实现了角色移动、多段连击、跳跃滑翔、敌人战斗、物品拾取与背包系统。

### 操作方式

| 按键 | 功能 |
|------|------|
| W/A/S/D | 移动 |
| 鼠标 | 视角旋转 |
| 鼠标左键 | 轻击（连续按下可触发三段连击） |
| 鼠标右键 | 重击 |
| 空格 | 跳跃 / 二段跳（滑翔） |
| R | 模拟受到攻击 |
| E | 拾取物品 |
| B | 打开/关闭背包 |

- 血量 ≤ 0 时角色死亡，5 秒后自动复活
- 靠近可拾取物品时出现提示，按 E 拾取

### 核心技术

#### 1. 有限状态机 (FSM)

整个角色行为系统基于**有限状态机**架构设计。`StateBase` 作为所有状态的基类，通过 `ChangeState<T>()` 泛型方法实现状态之间的切换：

```
IdleState → RunState / JumpState / Attack1State / Attack4State / InteractiveState / DamageState
RunState → IdleState / JumpState / Attack1State / Attack4State / InteractiveState
JumpState → FlyState (二段跳) / IdleState
Attack1State → Attack2State → Attack3State (三段连击窗口)
DamageState → IdleState / DieState
DieState → IdleState (5秒后复活)
FlyState → IdleState (落地)
```

每个状态在 `OnEnable` 中设置动画参数和初始化逻辑，在 `OnDisable` 中清理状态，在 `Update` 中处理输入和状态转换条件。这种设计使得每个状态职责单一、逻辑清晰、易于扩展新状态。

#### 2. 三段连击系统

轻击攻击支持最多三次连续连击，实现方式是**输入窗口 + 动画过渡**：

- `Attack1State`：在 `changeTime` 窗口内（约 0.5 秒）检测玩家是否再次按下攻击键，若按下则 `animator.SetBool("1,2", true)` 并切换到 `Attack2State`
- `Attack2State`：同样在窗口内检测，切换到 `Attack3State`
- `Attack3State`：不再接受连击输入，动画结束后回到 `IdleState`
- 每个攻击状态通过 `triggerHit` 标志防止同一段攻击造成多次伤害

#### 3. 伤害判定（球形检测 + 扇形方向过滤）

攻击命中检测采用 `Physics.OverlapSphere` 球形范围检测 + 方向角度过滤：

- 以玩家位置为中心，2 单位半径进行球形物理检测
- 对检测到的敌人，计算「玩家朝向」与「玩家到敌人方向」的夹角
- 只有前方 90° 扇形范围内（±45°）的敌人才会受到伤害
- 轻击造成 1 倍攻击力伤害，重击造成 3 倍攻击力伤害
- 动画事件 `Hit()` 在动画关键帧触发，通过 `AnimationEventReceiver` 桥接到 `PlayerControl.OnAnimationHit()` → 当前状态的 `Hit()`

#### 4. 跳越与滑翔系统

- **一段跳**：`JumpState` 给 Rigidbody 施加向上冲量（`AddForce(Vector3.up * 200)`），持续 0.6 秒
- **二段跳（滑翔）**：在一段跳的前 0.4 秒内再次按空格，切换到 `FlyState`
- **滑翔**：`FlyState` 将角色质量设为 0.3（变轻），施加额外上升力，使用自定义重力（`gravity = -3`），展开翅膀特效，缩小碰撞体半径
- 落地检测通过 `IsGroundControl` 的 Trigger 检测实现

#### 5. 生命值与死亡复活

- 玩家 `PlayerControl` 维护 HP 属性，使用 C# 属性访问器自动钳制范围 [0, MaxHp] 并同步更新 UI
- `StateBase.GetHit()` 统一处理受伤逻辑：HP > 0 进入 `DamageState`（受伤硬直动画）；HP ≤ 0 进入 `DieState`
- `DieState` 播放死亡动画，1 秒后显示治疗特效，5 秒后自动复活并回满血量
- `HpBar`（Slider 血条）和 `PlayerPanelControl`（心形图标）两种 UI 显示方式

#### 6. 摄像机控制

- 使用 `CameraChat` 脚本实现第三人称环绕摄像机
- 通过 Unity New Input System 读取鼠标输入，绕玩家 Y 轴旋转
- `Cursor.lockState = Locked` 锁定鼠标，提供 FPS 风格的视角控制
- 使用四元数旋转计算偏移位置，`LookAt` 始终注视角色

#### 7. 物品拾取与背包系统

- **物品数据**：使用 `ScriptableObject` 定义物品（`ItemData`），包含 ID、名称、图标、类型（武器/护甲/消耗品/材料/任务）、品质（普通到传说）、描述
- **拾取**：`ItemPickup` 通过 Trigger 检测玩家进入范围，显示 "按 E 拾取" UI 提示，按 E 后将物品加入背包并销毁场景物体
- **背包**：`Inventory` 维护 `List<ItemData>`，容量上限 20；`InventoryUI` 按 B 键打开/关闭背包面板，动态生成 `ItemSlotUI` 显示物品图标
- **世界物品展示**：`ItemRotator` 让场景中的物品绕 Y 轴旋转，并可随机初始角度，使物品看起来不呆板

#### 8. 输入系统

使用 **Unity New Input System**（`InputActionAsset`）统一管理所有输入：

| Action | 绑定 |
|--------|------|
| Move | WASD / 摇杆 |
| Camera | 鼠标位移 |
| Attack1 | 鼠标左键 |
| Attack2 | 鼠标右键 |
| Jump | 空格 |
| Interactive | E 键 |

`InputManager` 以单例模式封装，通过属性将底层 API（`WasPerformedThisFrame` / `ReadValue<Vector2>`）转换为游戏层友好的接口。

#### 9. 角色移动（相机相对方向）

移动方向基于摄像机朝向计算，而非世界坐标轴：

```csharp
var dir = Quaternion.LookRotation(camera.transform.forward) 
        * new Vector3(input.x, 0, input.y).normalized;
```

这使得角色始终朝屏幕前方移动，符合第三人称操作直觉。

### 项目结构

```
3DRPGScript/
├── PlayerControl.cs        # 玩家属性（HP、攻击力）
├── InputManager.cs         # 输入系统封装（New Input System）
├── CameraChat.cs           # 第三人称环绕摄像机
├── CameraFollow.cs         # 跟随摄像机（备用）
├── CameraControl.cs        # 基础摄像机旋转控制
├── EnemyControl.cs         # 敌人控制
├── IsGroundControl.cs      # 地面检测（Trigger）
├── IsGrounded.cs           # 地面检测（备用）
├── FootStepEvent.cs        # 脚步声动画事件
├── InteractiveBase.cs      # 可交互物体基类
├── InteractiveTest.cs      # 交互测试
├── State/
│   ├── StateBase.cs            # 状态机基类
│   ├── IdleState.cs            # 待机状态
│   ├── RunState.cs             # 移动状态
│   ├── JumpState.cs            # 跳跃状态
│   ├── FlyState.cs             # 滑翔状态（二段跳）
│   ├── Attack1State.cs         # 轻击第一段
│   ├── Attack2State.cs         # 轻击第二段
│   ├── Attack3State.cs         # 轻击第三段
│   ├── Attack4State.cs         # 重击
│   ├── DamageState.cs          # 受伤硬直
│   ├── DieState.cs             # 死亡/复活
│   ├── InteractiveState.cs     # 交互状态
│   └── AnimationEventReceiver.cs  # 动画事件桥接
├── Item/
│   ├── ItemData.cs             # 物品数据（ScriptableObject）
│   ├── Inventory.cs            # 背包逻辑
│   ├── ItemPickup.cs           # 物品拾取
│   └── ItemRotator.cs          # 物品展示旋转
└── UI/
    ├── UIManager.cs            # 主菜单场景管理
    ├── PlayerPanelControl.cs   # 心形血条面板
    ├── HpBar.cs                # Slider 血条
    ├── PickupUI.cs             # 拾取提示 UI
    ├── InventoryUI.cs          # 背包界面
    └── ItemSlotUI.cs           # 背包物品槽
```

---

## 项目二：2DRemove — 2D 三层卡牌消除游戏

参考"羊了个羊"玩法的 2D 卡牌消除游戏，点击未被遮挡的卡牌将其加入收集槽，三个相同卡牌自动消除。

### 操作方式

| 操作 | 功能 |
|------|------|
| 鼠标左键点击 | 选择卡牌 |
| 刷新按钮 | 重新开始当前关卡 |
| 设置按钮 | 音量调节 |

### 核心机制

#### 1. 三层布局与遮挡判断

- 使用 3 层卡牌堆叠（每层偏移 0.1 / 0.2 单位），通过 `sortingOrder` 控制渲染层级（0-99 → 100-199 → 200-299）
- **遮挡检测**：`ItemControl.Check()` 使用 `Physics2D.OverlapBoxAll` 检测同位置是否有更高 `sortingOrder` 的卡牌，若有则变灰（不可点击）
- 卡牌被取走后，调用 `Invoke("Refresh", 0.1f)` 重新检查所有剩余卡牌的遮挡状态

#### 2. 三消匹配逻辑

- 点击卡牌 → `CardManager.Add()` 将卡牌加入收集槽
- 每次加入后检查末尾 3 张：若三张名称完全相同，从槽中移除并销毁
- 收集槽超过 7 张且未触发消除 → 游戏失败

#### 3. 洗牌算法

`Shuffle()` 方法确保：
- 每 3 张为一组，组内 ID 相同（保证可消除）
- 检查 `width % 3 == 0` 或 `height % 3 == 0`，确保布局可整除
- 使用 Fisher-Yates 洗牌打乱顺序

#### 4. 点击穿透处理

同一位置多层卡牌点击时，通过 `Physics2D.RaycastAll` 获取所有命中，选择 `sortingOrder` 最大的卡牌（最顶层未被遮挡的）响应点击。

### 项目结构

```
2Dremovecript/
├── GameManage.cs          # 游戏主逻辑（布局生成、洗牌、点击分发）
├── CardControl.cs         # 收集槽卡牌显示
├── CardManager.cs         # 卡牌收集/消除管理
├── ItemControl.cs         # 游戏区卡牌控制（遮挡检测、点击响应）
├── GameUIManager.cs       # 游戏内 UI（刷新、设置、音量）
├── UIManager.cs           # 主菜单
└── Resolution.cs          # 画布分辨率适配
```
