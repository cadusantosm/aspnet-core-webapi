﻿using Demo.Application.GraphQL.Types.Character.Models;
using Framework.Data.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Application.Data.MySql.Entities
{
    /// <summary>
    /// Entidade de personagem
    /// </summary>
    public class CharacterEntity : EFEntityBase
    {
        public CharacterEntity()
        {

        }

        public CharacterEntity(CharacterModel graphModel)
        {
            Name = graphModel.Name;
            Kind = graphModel.Kind;
            BirthDate = graphModel.BirthDate;
        }

        /// <summary>
        /// ID do registro
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// Nome do personagem
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// Especie
        /// </summary>
        public ECharecterKind Kind { get; set; }

        /// <summary>
        /// Lista de parentes
        /// </summary>
        public List<FamilyEntity> Relatives { get; set; }
    }

    /// <summary>
    /// Enumerador de especies
    /// </summary>
    public enum ECharecterKind
    {
        /// <summary>
        /// Humano
        /// </summary>
        Human = 1,

        /// <summary>
        /// Sayajin
        /// </summary>
        Sayajin
    }
}