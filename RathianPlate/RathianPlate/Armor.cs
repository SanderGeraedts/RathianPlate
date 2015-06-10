using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    public class Armor
    {
        #region fields

        private int id;
        private string name;
        private int value;
        private int rarity;
        private string rank;
        private string piece;
        private int slots;
        private string weaponType;
        private int baseDefense;
        private int fireResis;
        private int waterResis;
        private int thunderResis;
        private int iceResis;
        private int dragonResis;

        #endregion

        #region properties
        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public int Value { get { return value; } }
        public int Rarity { get { return rarity; } }
        public string Rank { get { return rank; } }
        public string Piece { get { return piece; } }
        public int Slots { get { return slots; } }
        public string WeaponType { get { return weaponType; } }
        public int BaseDefense { get { return baseDefense; } }
        public int FireResis { get { return fireResis; } }
        public int WaterResis { get { return waterResis; } }
        public int ThunderResis { get { return thunderResis; } }
        public int IceResis { get { return iceResis; } }
        public int DragonResis { get { return dragonResis; } }
        #endregion

        #region constructor

        public Armor(int id, string name, int value, int rarity, string rank, string piece, int slots, string weaponType,
            int baseDefense, int fireResis, int waterResis, int thunderResis, int iceResis, int dragonResis)
        {
            this.id = id;
            this.name = name;
            this.value = value;
            this.rarity = rarity;
            this.rank = rank;
            this.piece = piece;
            this.slots = slots;
            this.weaponType = weaponType;
            this.baseDefense = baseDefense;
            this.fireResis = fireResis;
            this.waterResis = waterResis;
            this.thunderResis = thunderResis;
            this.iceResis = iceResis;
            this.dragonResis = dragonResis;
        }
        #endregion

        #region private methods

        #endregion

        #region public methods

        #endregion
    }
}