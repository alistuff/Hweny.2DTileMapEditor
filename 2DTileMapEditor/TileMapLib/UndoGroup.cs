using System;
using System.Collections.Generic;
using System.Text;

namespace TileMapLib
{
    public class UndoGroup : List<UndoTile>
    {
        public UndoGroup()
        {
        }

        public void AddToGroup(UndoTile tile, bool checkExists)
        {
            if (checkExists && Exists(tile)) return;
            this.Add(tile);
        }

        public UndoGroup Populate(EditableMap map)
        {
            UndoGroup newGroup = new UndoGroup();
            bool result = false;
            foreach (UndoTile tile in this)
            {
                newGroup.Add(new UndoTile
                    (
                    map.GetTile(tile.X, tile.Y, tile.LayerId),
                    tile.X, tile.Y, tile.LayerId
                    ));

                result = map.SetTile(tile.Tile, tile.X, tile.Y, tile.LayerId);

                if (!result)
                {
                    newGroup.Clear();
                    break;
                }
            }
            return newGroup;
        }

        private bool Exists(UndoTile tile)
        {
            for (int i = 0; i < this.Count; i++)
            {
                UndoTile dest = this[i];
                if (dest != null)
                {
                    if (dest.LayerId == tile.LayerId &&
                        (dest.X == tile.X && dest.Y == tile.Y))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
