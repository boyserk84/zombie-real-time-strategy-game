using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;

namespace ZRTSMapEditor
{
    public class PlayerDataGridAdapterCommitter : MapEditorCommand
    {
        private List<PlayerDataGridAdapter> adapters;

        private PlayerDataGridAdapterCommitter()
        {
        }

        public PlayerDataGridAdapterCommitter(List<PlayerDataGridAdapter> adapters)
        {
            this.adapters = adapters;
        }

        public void Do()
        {
            foreach (PlayerDataGridAdapter adapter in adapters)
            {
                adapter.Do();
            }
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public bool CanBeDone()
        {
            bool canBeDone = true;
            Dictionary<string, Boolean> names = new Dictionary<string, Boolean>();
            int removed = 0;
            foreach (PlayerDataGridAdapter adapter in adapters)
            {
                canBeDone = canBeDone && adapter.CanBeDone();
                if (!canBeDone)
                {
                    break;
                }
                if (adapter.RemovedMember)
                {
                    removed++;
                }
                else
                {
                    names[adapter.Player_Name] = true;
                }
            }
            if (canBeDone)
            {
                canBeDone = (names.Count == (adapters.Count - removed));
            }
            return canBeDone;
        }
    }
}
