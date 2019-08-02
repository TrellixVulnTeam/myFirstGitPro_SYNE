﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackMole
{
    public class NpcStatePlayPhone : NpcStatePlay
    {

        public override void Enter(NpcActor owner)
        {
            base.Enter(owner);
            owner.ChangeState(NpcState.PlayPhone);
            if (owner.IsBossShow)
            {
                owner.ShowBoom(true);
            }
        }

        public override bool OnMessage(NpcActor owner, IStateEvent t)
        {
            switch (t.EventType)
            {
                case StateEventType.BossCome:
                    owner.ShowBoom(true);
                    return true;
                case StateEventType.BossLeave:
                    owner.ShowBoom(false);
                    return true;
                case StateEventType.BossAbuse:
                    EventDispatcher.TriggerEvent(BattleEvent.BossAngry);
                    owner.StateMachine.ChangeState(new NpcStateScare());
                    return true;
                case StateEventType.BossHit:
                    owner.ShowWater();
                    owner.RefreshWorkSate(WorkState.HitToWork);
                    EventDispatcher.TriggerEvent(BattleEvent.BossAngry);
                    owner.StateMachine.ChangeState(new NpcStateWork());
                    return true;
            }

            return false;
        }
    }
}


