using DataModels;
using LinqToDB;
using LinqToDB.Data;
 

namespace BMS.Infrastructure
{
    public class AppDB : DataConnection
    {
        public AppDB(string name = "AppDB") : base(name) { }

        #region BMS
        public ITable<TblAccountHead> TblAccountHeads { get { return this.GetTable<TblAccountHead>(); } }
        public ITable<TblBuildingInformation> TblBuildingInformation { get { return this.GetTable<TblBuildingInformation>(); } }
        public ITable<TblCity> TblCities { get { return this.GetTable<TblCity>(); } }
        public ITable<TblFlatInformation> TblFlatInformation { get { return this.GetTable<TblFlatInformation>(); } }
        public ITable<TblIncomeExpence> TblIncomeExpences { get { return this.GetTable<TblIncomeExpence>(); } }
        public ITable<TblUser> TblUsers { get { return this.GetTable<TblUser>(); } }
        public ITable<TblUserBuildingInfo> TblUserBuildingInfo { get { return this.GetTable<TblUserBuildingInfo>(); } }

        #endregion

    }
}
