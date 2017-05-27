using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace JD.AutoRunService.SynchronousData
{
    public class MyGenericDataReader<T> : IDataReader where T : class
    {

        // The enumerator for the IEnumerable<T>passed to the constructor 
        //for this instance.        
        private IEnumerator<T> enumerator = null;

        //List of all public fields in <T>
        private List<FieldInfo> fields = new List<FieldInfo>();

        public MyGenericDataReader(IEnumerable<T> enumerator)
        {
            this.enumerator = enumerator.GetEnumerator();

            //Find the enumerator of all public fields
            foreach (FieldInfo fieldinfo in typeof(T).GetFields(
                BindingFlags.Instance |
                BindingFlags.Public))
            {
                fields.Add(fieldinfo);
            }
        }

        #region IDataReader Interface Implementation
        //Advances the enumerator to the next record.
        public bool Read()
        {
            return enumerator.MoveNext();
        }

        //Closes the enumerator Object.
        public void Close()
        {
            enumerator.Dispose();
        }

        #endregion

        #region IDataReader Not Implemented
        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }


        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDisposable Interface Implementation

        public void Dispose()
        {
            Close();
        }

        #endregion

        #region IDataRecord Interface Implementation
        //Gets the number of fields (columns) in the current object.
        public int FieldCount
        {
            get { return fields.Count; }
        }

        //Gets the name of the current field.
        public string GetName(int i)
        {
            return fields[i].Name;
        }

        //Return the value of the current field.
        public object GetValue(int i)
        {
            return fields[i].GetValue(enumerator.Current);
        }

        // Gets the System.Type information corresponding to the type of 
        //System.Object that would be returned from GetValue(System.Int32).
        public Type GetFieldType(int i)
        {
            return fields[i].FieldType;
        }

        #endregion

        #region IDataRecord Members              

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset,
byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset,
char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public object this[int i]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
