using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.EventHandlers;
using ZRTSModel.GameModel;

namespace ZRTSModel
{
    /// <summary>
    /// A particular unit.  Contains an action queue.
    /// </summary>
    [Serializable()]
    public class UnitComponent : ModelComponent
    {
        public event UnitHPChangedHandler HPChangedEventHandlers;
        public event UnitMovedHandler MovedEventHandlers;

        private ActionQueue actionQueue;

        public UnitComponent()
        {
            actionQueue = new ActionQueue();
            AddChild(actionQueue);
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private short maxHealth = 100;       // The maximum health of a unit.

        public short MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }
        private float speed = 0.1f;          // How much a Unit should move during a move cycle.

        private short currentHealth;

        public short CurrentHealth
        {
            get { return currentHealth; }
            set 
            { 
                short prevHealth = currentHealth;
                currentHealth = value;
                if (HPChangedEventHandlers != null)
                {
                    UnitHPChangedEventArgs args = new UnitHPChangedEventArgs();
                    args.OldHP = (int) prevHealth;
                    args.NewHP = (int) currentHealth;
                    args.Unit = this;
                    HPChangedEventHandlers(this, args);
                }
            }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private float attackRange = 5.0f;    // How far a Unit can attack

        public float AttackRange
        {
            get { return attackRange; }
            set { attackRange = value; }
        }
        private short attack = 10;           // How much damage a Unit does when it attacks.

        public short Attack
        {
            get { return attack; }
            set { attack = value; }
        }
        private byte attackTicks = 10;       // How many ticks occur per attack cycle.

        public byte AttackTicks
        {
            get { return attackTicks; }
            set { attackTicks = value; }
        }
        private float visibilityRange = 6.0f;// How far can the unit see.

        public float VisibilityRange
        {
            get { return visibilityRange; }
            set { visibilityRange = value; }
        }
        private byte buildSpeed = 30;        // How much health a building gets per build cycle the Unit completes when building or repairing a building.

        public byte BuildSpeed
        {
            get { return buildSpeed; }
            set { buildSpeed = value; }
        }

        /** UNIT ABILITIES **/
        private bool canAttack = false;      // Can this Unit attack?

        public bool CanAttack
        {
            get { return canAttack; }
            set { canAttack = value; }
        }
        private bool canHarvest = false;     // Can this Unit harvest resources?

        public bool CanHarvest
        {
            get { return canHarvest; }
            set { canHarvest = value; }
        }
        private bool canBuild = false;       // Can this Unit build buildings?

        public bool CanBuild
        {
            get { return canBuild; }
            set { canBuild = value; }
        }
        private bool isZombie = false;       // Is this Unit a zombie?

        public bool IsZombie
        {
            get { return isZombie; }
            set { isZombie = value; }
        }

        private CellComponent location;

        public CellComponent Location
        {
            get { return location; }
        }

        private int orientation;

        public int Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }



        private PointF pointLocation;

        public PointF PointLocation
        {
            get { return pointLocation; }
            set 
            {
                UnitMovedEventArgs args = new UnitMovedEventArgs();
                args.Unit = this;
                args.OldPoint = pointLocation;
                args.NewPoint = value;

                pointLocation = value;
                if (location != null)
                {
                    if (location.X != (int)pointLocation.X || location.Y != (int)pointLocation.Y)
                    {
                        location.EntitiesContainedWithin.Remove(this);
                        if (pointLocation != null)
                        {
                            Map map = (Map)location.Parent;
                            location = map.GetCellAt((int)pointLocation.X, (int)pointLocation.Y);
                            location.EntitiesContainedWithin.Add(this);
                        }
                    }
                }
                else if (pointLocation != null)
                {
                    Map map = ((Gameworld)(Parent.Parent.Parent.Parent)).GetMap();
                    location = map.GetCellAt((int)pointLocation.X, (int)pointLocation.Y);
                    location.EntitiesContainedWithin.Add(this);
                }
                if (MovedEventHandlers != null)
                {
                    MovedEventHandlers(this, args);
                }

            }
        }

        public ActionQueue GetActionQueue()
        {
            return actionQueue;
        }

        public override void RemoveChild(ModelComponent child)
        {
            if (child != actionQueue)
                base.RemoveChild(child);
        }

        public override void Accept(ModelComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
