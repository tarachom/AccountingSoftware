﻿

/*
 *
 * Конфігурації "ConfTrade 1.1"
 * Автор Yurik
 * Дата конфігурації: 07.02.2020 19:30:32
 *
 */

using System;
using System.Collections.Generic;
using AccountingSoftware;

namespace ConfTrade_v1_1
{
    static class Config
    {
        public static Kernel Kernel { get; set; }
    }
    
    #region DIRECTORY "Tovary"
    
    /// <summary> 
    /// Довідник Товари
    /// </summary>
    class Tovary_Objest : DirectoryObject
    {
        public Tovary_Objest() : base(Config.Kernel, "tovary",
             new string[] { "name", "code", "description" }) 
        {
            Name = new string[] { };
            Code = "";
            Description = "";
            
            //Табличні частини
            Ceny_TablePart = new Tovary_Ceny_TablePart(this);
            
        }
        
        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Name = (base.FieldValue["name"] != DBNull.Value) ? (string[])base.FieldValue["name"] : new string[] { };
                Code = base.FieldValue["code"].ToString();
                Description = base.FieldValue["description"].ToString();
                
                return true;
            }
            else
                return false;
        }
        
        public void Save()
        {
            base.FieldValue["name"] = Name;
            base.FieldValue["code"] = Code;
            base.FieldValue["description"] = Description;
            
            BaseSave();
        }
        
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public Tovary_Pointer GetDirectoryPointer()
        {
            Tovary_Pointer directoryPointer = new Tovary_Pointer(UnigueID.UGuid);
            return directoryPointer;
        }
        
        public string[] Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        
        //Табличні частини
        public Tovary_Ceny_TablePart Ceny_TablePart { get; set; }
        
    }
    
    /// <summary> 
    /// Довідник Товари
    /// </summary>
    class Tovary_Pointer : DirectoryPointer
    {
        public Tovary_Pointer(object uid = null) : base(Config.Kernel, "tovary")
        {
            if (uid != null && uid != DBNull.Value) base.Init(new UnigueID((Guid)uid), null);
        }

        public Tovary_Objest GetDirectoryObject()
        {
            Tovary_Objest TovaryObjestItem = new Tovary_Objest();
            TovaryObjestItem.Read(base.UnigueID);
            return TovaryObjestItem;
        }
    }
    
    /// <summary> 
    /// Довідник Товари
    /// </summary>
    class Tovary_Select : DirectorySelect
    {
        public Tovary_Select() : base(Config.Kernel, "tovary") { }
    
        public bool Select() 
        { 
            return base.BaseSelect();
        }
        
        public bool SelectSingle()
        {
            if (base.BaseSelectSingle())
            {
                MoveNext();
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }
        
        public bool MoveNext()
        {
            if (MoveToPosition())
            {
                Current = new Tovary_Pointer();
                Current.Init(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields);
                return true;
            }
            else
            {
                Current = null;
                return false;
            }
        }

        public Tovary_Pointer Current { get; private set; }
    }
    
      
    /// <summary>
    /// Таблична частина Ціни
    /// </summary>
    class Tovary_Ceny_TablePart : DirectoryTablePart
    {
        public Tovary_Ceny_TablePart(Tovary_Objest owner) : base(Config.Kernel, "tovary_ceny_tablepart",
             new string[] { "name", "cena" }) 
        {
            Owner = owner;
            Records = new List<Tovary_Ceny_TablePartRecord>();
        }
        
        public Tovary_Objest Owner { get; private set; }
        
        public List<Tovary_Ceny_TablePartRecord> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            base.FieldValueList.Clear();

            base.BaseRead(Owner.UnigueID);

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Tovary_Ceny_TablePartRecord record = new Tovary_Ceny_TablePartRecord();

                record.Name = fieldValue["name"].ToString();
                record.Cena = (fieldValue["cena"] != DBNull.Value) ? (decimal)fieldValue["cena"] : 0;
                
                Records.Add(record);
            }
        }
        
        /// <summary>
        /// Зберегти колекцію Records в базу.
        /// </summary>
        /// <param name="clear_all_before_save">
        /// Перед записом колекції, попередні записи видаляються з бази даних.
        /// Щоб не видаляти треба поставити clear_all_before_save = false.
        /// Це корисно коли потрібно добавити нові записи без зчитування всієї колекції.
        /// </param>
        public void Save(bool clear_all_before_save = true) 
        {
            if (Records.Count > 0)
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete(Owner.UnigueID);

                foreach (Tovary_Ceny_TablePartRecord record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("name", record.Name);
                    fieldValue.Add("cena", record.Cena);
                    
                    base.BaseSave(Owner.UnigueID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        }
        
        public void Clear()
        {
            base.BaseDelete(Owner.UnigueID);
        }
    }
    
    /// <summary> 
    /// Таблична частина Ціни
    /// </summary>
    class Tovary_Ceny_TablePartRecord : DirectoryTablePartRecord
    {
        public Tovary_Ceny_TablePartRecord()
        {
            Name = "";
            Cena = 0;
            
        }
        
        public Tovary_Ceny_TablePartRecord(
            string _Name = "", decimal _Cena = 0)
        {
            Name = _Name;
            Cena = _Cena;
            
        }
        
        public string Name { get; set; }
        public decimal Cena { get; set; }
        
    }
      
    
    #endregion
    
}
  