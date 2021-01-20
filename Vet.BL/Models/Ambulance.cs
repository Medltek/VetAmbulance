using System;
using System.Collections.Generic;
using System.Text;
using VetAmbulance.DAL;
using VetAmbulance.DAL.Mappers;

namespace VetAmbulance.BL.Models
{
    public class Ambulance
    {
        private readonly AmbulanceMapper ambulanceMapper;

        public Ambulance()
        {
        }

        public Ambulance(AmbulanceMapper ambulanceMapper)
        {
            this.ambulanceMapper = ambulanceMapper;
        }

        public int Id { get; set; }

        public string Address { get; set; }



        public List<Vet> Vets { get; set; }

        public int OpeningHour { get; set; }

        public int ClosingHour { get; set; }

        public Ambulance GetById(int id)
        {
            var ambulance = ambulanceMapper.GetById(id);

            if (ambulance == null)
            {
                return null;
            }

            Id = ambulance.Id;
            Address = ambulance.Address;

            OpeningHour = ambulance.OpeningHour;
            ClosingHour = ambulance.ClosingHour;

            return this;
        }

        public List<Ambulance> GetAll()
        {
            var ambulances = ambulanceMapper.GetAll();

            var ambulancesList = new List<Ambulance>();

            if (ambulances == null)
            {
                return null;
            }

            foreach (var ambulance in ambulances)
            {
                ambulancesList.Add(new Ambulance()
                {
                    Id = ambulance.Id,
                    Address = ambulance.Address,

                    OpeningHour = ambulance.OpeningHour,
                    ClosingHour = ambulance.ClosingHour
                });
            }

            return ambulancesList;
        }

        public List<Vet> GetVets(int ambulanceId)
        {
            var vets = ambulanceMapper.GetVets(ambulanceId);

            if (vets == null)
            {
                return null;
            }

            var vetsList = new List<Vet>();

            foreach (var vet in vets)
            {
                vetsList.Add(new Vet()
                {
                    Id = vet.Id,
                    Name = vet.Name,

                    AmbulanceId = vet.AmbulanceId
                });
            }

            return vetsList;
        }

        public bool Insert(AmbulanceDTO ambulanceDto)
        {
            try
            {
                var ambulance = new DAL.Ambulance()
                {
                    Address = ambulanceDto.Address,
                    ClosingHour = ambulanceDto.ClosingHour,
                    OpeningHour = ambulanceDto.OpeningHour,

                };

                ambulanceMapper.Insert(ambulance);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(AmbulanceDTO ambulanceDto, int id)
        {
            try
            {
                var ambulance = new DAL.Ambulance()
                {
                    Id = id,
                    Address = ambulanceDto.Address,
                    ClosingHour = ambulanceDto.ClosingHour,
                    OpeningHour = ambulanceDto.OpeningHour

                };

                ambulanceMapper.Edit(ambulance);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(int id)
        {
            ambulanceMapper.Delete(id);
        }
    }
}
