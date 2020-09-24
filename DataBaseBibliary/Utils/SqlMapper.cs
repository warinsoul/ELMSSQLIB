using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBibliary.Utils
{
    public static class SqlMapper

    {

        private static readonly Dictionary<Type, DbType> TypeToDbType  = new Dictionary<Type, DbType>
                {
                  {typeof (byte), DbType.Byte},
                  {typeof (sbyte), DbType.SByte},
                  {typeof (short), DbType.Int16},
                  {typeof (ushort), DbType.UInt16},
                  {typeof (int), DbType.Int32},
                  {typeof (uint), DbType.UInt32},
                  {typeof (long), DbType.Int64},
                  {typeof (ulong), DbType.UInt64},
                  {typeof (float), DbType.Single},
                  {typeof (double), DbType.Double},
                  {typeof (decimal), DbType.Decimal},
                  {typeof (bool), DbType.Boolean},
                  {typeof (string), DbType.AnsiString},
                  {typeof (char), DbType.StringFixedLength},
                  {typeof (Guid), DbType.Guid},
                  {typeof (DateTime), DbType.DateTime},
                  {typeof (DateTimeOffset), DbType.DateTimeOffset},
                  {typeof (byte[]), DbType.Binary},
                  {typeof (byte?), DbType.Byte},
                  {typeof (sbyte?), DbType.SByte},
                  {typeof (short?), DbType.Int16},
                  {typeof (ushort?), DbType.UInt16},
                  {typeof (int?), DbType.Int32},
                  {typeof (uint?), DbType.UInt32},
                  {typeof (long?), DbType.Int64},
                  {typeof (ulong?), DbType.UInt64},
                  {typeof (float?), DbType.Single},
                  {typeof (double?), DbType.Double},
                  {typeof (decimal?), DbType.Decimal},
                  {typeof (bool?), DbType.Boolean},
                  {typeof (char?), DbType.StringFixedLength},
                  {typeof (Guid?), DbType.Guid},
                  {typeof (DateTime?), DbType.DateTime},
                  {typeof (DateTimeOffset?), DbType.DateTimeOffset},
                  };

        private static readonly Dictionary<DbType, Type> DbTypeMapToType
           = new Dictionary<DbType, Type>
                  {
                  {DbType.Byte, typeof (byte)},
                  {DbType.SByte, typeof (sbyte)},
                 {DbType.Int16, typeof (short)},
                  {DbType.UInt16, typeof (ushort)},
                  {DbType.Int32, typeof (int)},
                  {DbType.UInt32, typeof (uint)},
                  {DbType.Int64, typeof (long)},
                 {DbType.UInt64, typeof (ulong)},
                  {DbType.Single, typeof (float)},
                  {DbType.Double, typeof (double)},
                  {DbType.Decimal, typeof (decimal)},
                  {DbType.Boolean, typeof (bool)},
                  {DbType.String, typeof (string)},
                  {DbType.StringFixedLength, typeof (char)},
                  {DbType.Guid, typeof (Guid)},
                  {DbType.DateTime, typeof (DateTime)},
                  {DbType.DateTimeOffset, typeof (DateTimeOffset)},
                  {DbType.Binary, typeof (byte[])}
                  };



        private static readonly Dictionary<DbType, Type> DbTypeMapToNullableType

            = new Dictionary<DbType, Type>

                  {

                  {DbType.Byte, typeof (byte?)},

                  {DbType.SByte, typeof (sbyte?)},

                  {DbType.Int16, typeof (short?)},

                  {DbType.UInt16, typeof (ushort?)},

                  {DbType.Int32, typeof (int?)},

                  {DbType.UInt32, typeof (uint?)},

                  {DbType.Int64, typeof (long?)},

                  {DbType.UInt64, typeof (ulong?)},

                  {DbType.Single, typeof (float?)},

                  {DbType.Double, typeof (double?)},

                  {DbType.Decimal, typeof (decimal?)},

                  {DbType.Boolean, typeof (bool?)},

                  {DbType.StringFixedLength, typeof (char?)},

                  {DbType.Guid, typeof (Guid?)},

                  {DbType.DateTime, typeof (DateTime?)},

                  {DbType.DateTimeOffset, typeof (DateTimeOffset?)},

                  {DbType.Binary, typeof(byte[])}

                  };



        public static DbType ToDbType(this Type type)
        {
            DbType dbType;
            if (TypeToDbType.TryGetValue(type, out dbType)) return dbType;
            throw new ArgumentOutOfRangeException("type", type, "Cannot map the Type to DbType");
        }



        public static Type ToClrType(this DbType dbType)

        {

            Type type;

            if (DbTypeMapToType.TryGetValue(dbType, out type)) return type;

            throw new ArgumentOutOfRangeException("dbType", dbType, "Cannot map the DbType to Type");

        }



        public static Type ToNullableClrType(this DbType dbType)

        {

            Type type;

            if (DbTypeMapToNullableType.TryGetValue(dbType, out type)) return type;

            throw new ArgumentOutOfRangeException("dbType", dbType, "Cannot map the DbType to Nullable Type");

        }



      

    }

}
