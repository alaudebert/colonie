using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Athletic : Settler
    {
        public static int LevelIncrease = 1;
        private static int _nb;
        private int _athleticNb;
        private int _level;
        public string Sport { get; set; }
        public string Nationality { get; set; }
        private int Session;
        private static string _type;
        private bool IsTraining { get; set; }
        public Coach myCoach { get; set; }
        private Village _village;

        /// <summary>
        /// Constructor that allows to create a sportsman
        /// </summary>
        /// <param name="nationality">Nationality of the athlete</param>
        /// <param name="sport">Sport that practices the athlete</param>
        /// <param name="village">Village in which we create the athlete</param>
        public Athletic(string nationality, string sport, Village village) : base()
        {
            Session = 0;
            _level = 0;
            _village = village;
            _buildings = new Building[3];
            myCoach = null;
            IsTraining = false;
            _type = "A";
            SettlerType = _type;
            _nb++;
            _athleticNb = _nb;

            _id = _type + _athleticNb.ToString();
            Nationality = nationality;
            Sport = sport;
        }

        public int AthleticNb
        {
            get { return _athleticNb; }
        }

        /// <summary>
        /// Returns the athlete's type
        /// </summary>
        public string Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Returns the level of the athlete
        /// </summary>
        public int getLevel()
        {
            return _level;
        }

        /// <summary>
        /// Modification of the Play method of the colonists, allowing to specify whether or not they should go to train, as well as the level of energy and hunger they lose
        /// </summary>
        /// <param name="GameBoardSettler">Settlers' game board</param>
        /// <param name="turnNb">Turn of the game where we play our settlers</param>
        public override void Play(List<Settler>[,] GameBoardSettler, int turnNb)
        {

            if (IsTraining)
            {
                DecreasingEnergy = 2;
                DecreasingHunger = 3;
                Train(myCoach, turnNb);
            }

            if (Math.Abs(Itinerary[0]) + Math.Abs(Itinerary[1]) != 0)
            {
                GameBoardSettler[_x, _y].Remove(this);
                this.Move();
                GameBoardSettler[_x, _y].Add(this);
            }

            EnergyState -= DecreasingEnergy;
            HungerState -= DecreasingHunger;

            if (EnergyState <= 0)
            {
                EnergyState = 0;
                if (!IsInActivity)
                {
                    this.CalculatingItinerary(_buildings[0].X, _buildings[0].Y);
                    NbTunrBeforeAvailable = Math.Abs(Itinerary[0]) + Math.Abs(Itinerary[1]) + 2 + turnNb;
                    IsInActivity = true;
                }
                else
                {
                    if (NbTunrBeforeAvailable == turnNb)
                    {
                        EnergyState = Energy;
                        IsInActivity = false;
                        NbTunrBeforeAvailable = 0;
                    }
                }
            }
            if (HungerState <= 0)
            {
                HungerState = 0;
                if (!IsInActivity)
                {
                    this.CalculatingItinerary(Buildings[1].X, Buildings[1].Y);
                    NbTunrBeforeAvailable = Math.Abs(Itinerary[0]) + Math.Abs(Itinerary[1]) + 2 + turnNb;
                    IsInActivity = true;
                }
                else
                {
                    if (NbTunrBeforeAvailable == turnNb)
                    {
                        HungerState = Hunger;
                        IsInActivity = false;
                        NbTunrBeforeAvailable = 0;
                    }
                }
            }

        }

        /// <summary>
        /// Allows you to train a sportsman with a coach
        /// </summary>
        /// <param name="coach">Coach who trains him</param>
        /// <param name="turnNb">Turn where it is trained</param>
        public void Train(Coach coach, int turnNb)
        {
            if (!IsInActivity)
            {
                Building building = Buildings[2];
                CalculatingItinerary(building.X, building.Y);
                int itinerary = Math.Abs(Itinerary[0]) + Math.Abs(Itinerary[1]);
                NbTunrBeforeAvailable = 4 + turnNb + itinerary;
                IsInActivity = true;
                IsTraining = true;
                if (coach != null)
                {
                    myCoach.CalculatingItinerary(building.X, building.Y);
                    coach.Lead(turnNb);
                }
            }
            
            if (NbTunrBeforeAvailable == turnNb) {
                if (coach != null)
                {
                    Session += 2;
                }
                else
                {
                    Session++;
                }
                if (Session >= 2)
                {
                    Session = 0;
                    _level += LevelIncrease;
                }
                if (_level >= 1)
                {
                    _village.ProfessionnelNb += 1;
                }
                IsInActivity = false;
                IsTraining = false;
                NbTunrBeforeAvailable = 0;
                DecreasingEnergy = 1;
                DecreasingHunger = 2;
            }
        }

        public override string ToString()
        {
            return base.ToString() + "Son niveau : " + getLevel() + "\nSon nombre de session : " + Session + "\n Sport qu'il pratique : "
                + Sport + "\n Nationalité : " + Nationality + "\n"; ;
        }

    }
}
