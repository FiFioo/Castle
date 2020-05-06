# Castle
Tower defence with hero game scripts 

> 预览


<img src="./images/角色模型1.png" width="60%" alt="玩家角色"> 玩家角色</img>

<img src="./images/角色模型2.png" width="60%" alt="怪物"> 怪物</img>


<img src="./images/角色模型3.png" width="60%" alt="炮塔"> 炮塔</img>

<img src="./images/关卡模块1.png" width="60%" alt="关卡"> 关卡</img>

<img src="./images/battle1.png" width="60%" alt="战斗1"> 战斗1</img>

<img src="./images/battle2.png" width="60%" alt="战斗2"> 战斗2</img>

<img src="./images/任务模块.png" width="60%" alt="任务展示"> 任务展示</img>

> 功能

* 已实现 ： 
  * 战斗部分 :
    * 敌人按照设定路线朝基地移动，接触基地自爆伤害基地 1 点血
    * 英雄进入敌人检测范围，敌人会向英雄移动并攻击，玩家点击普通攻击会走向发现的第一个敌人并攻击
  * 动作部分 :
    * 摇杆控制移动
    * 攻击、技能会互相打断，移动会打断攻击、技能
  * 防御塔部分 :
    * 种类 ： 2， 升级 ： 无， 同一个地方只能建一个
    * 防御塔点击建造，需要消耗金币，怪物死亡可以获得一定金币
    * 防御塔枪口朝向敌人，子弹向敌人移动，接触爆炸并伤害
  * 关卡部分 :
    * 进入关卡需要消耗 1 点体力
    * 通关才能解锁下一个关卡
  * 任务部分 :
    * 通关才能完成任务
    * 完成任务可以获得 1 点体力
  * 其他 :
    * 数据存储在本地，可以清除数据
    * 基地或者英雄死亡都会导致战斗失败
    * 音量可以控制并存储
