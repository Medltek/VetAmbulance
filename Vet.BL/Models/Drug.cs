using System;
using System.Collections.Generic;
using System.Text;
using VetAmbulance.DAL.Mappers;
namespace VetAmbulance.BL.Models
{
    public class Drug
    {
        private readonly DrugMapper drugMapper;

        public Drug()
        {
        }

        public Drug(DrugMapper drugMapper)
        {
            this.drugMapper = drugMapper;
        }

        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public string Name { get; set; }

        public Patient Patient1
        {
            get => default;
            set
            {
            }
        }

        public Drug GetById(int id)
        {
            var drug = drugMapper.GetById(id);

            if (drug == null)
            {
                return null;
            }

            Id = drug.Id;
            PatientId = drug.PatientId;
            Name = drug.Name;


            return this;
        }

        public List<Drug> GetAll()
        {
            var drugs = drugMapper.GetAll();

            var drugsList = new List<Drug>();

            if (drugs == null)
            {
                return null;
            }

            foreach (var drug in drugs)
            {
                drugsList.Add(new Drug()
                {
                    Id = drug.Id,
                    PatientId = drug.PatientId,
                    Name = drug.Name,

                });
            }

            return drugsList;
        }

        public bool Insert(DrugDTO drugDto)
        {
            try
            {
                var drug = new DAL.Drug()
                {
                    PatientId = drugDto.PatientId,

                    Name = drugDto.Name
                };

                drugMapper.Insert(drug);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(DrugDTO drugDto, int id)
        {
            try
            {
                var drug = new DAL.Drug()
                {
                    Id = id,
                    PatientId = drugDto.PatientId,

                    Name = drugDto.Name
                };

                drugMapper.Edit(drug);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(int id)
        {
            drugMapper.Delete(id);
        }

    }
}
