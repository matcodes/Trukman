using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region BaseEntity
    public class BaseEntity
    {
        public BaseEntity()
        {
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var baseEntity = (obj as BaseEntity);
            return ((baseEntity != null) && (this.Id.Equals(baseEntity.Id)));
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
    #endregion
}
