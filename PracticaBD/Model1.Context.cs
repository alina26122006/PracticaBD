﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PracticaBD
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CarRepairEntities : DbContext
    {
        public CarRepairEntities()
            : base("name=CarRepairEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Mechanics> Mechanics { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<WorkOrders> WorkOrders { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    
        [DbFunction("CarRepairEntities", "GetAvailableParts")]
        public virtual IQueryable<GetAvailableParts_Result> GetAvailableParts()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetAvailableParts_Result>("[CarRepairEntities].[GetAvailableParts]()");
        }
    
        [DbFunction("CarRepairEntities", "GetCustomerDetails")]
        public virtual IQueryable<GetCustomerDetails_Result> GetCustomerDetails(Nullable<int> customer_id)
        {
            var customer_idParameter = customer_id.HasValue ?
                new ObjectParameter("customer_id", customer_id) :
                new ObjectParameter("customer_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetCustomerDetails_Result>("[CarRepairEntities].[GetCustomerDetails](@customer_id)", customer_idParameter);
        }
    
        [DbFunction("CarRepairEntities", "GetPendingWorkOrders")]
        public virtual IQueryable<GetPendingWorkOrders_Result> GetPendingWorkOrders()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetPendingWorkOrders_Result>("[CarRepairEntities].[GetPendingWorkOrders]()");
        }
    
        public virtual int AddCustomer(string first_name, string last_name, string phone_number, string email, ObjectParameter new_customer_id)
        {
            var first_nameParameter = first_name != null ?
                new ObjectParameter("first_name", first_name) :
                new ObjectParameter("first_name", typeof(string));
    
            var last_nameParameter = last_name != null ?
                new ObjectParameter("last_name", last_name) :
                new ObjectParameter("last_name", typeof(string));
    
            var phone_numberParameter = phone_number != null ?
                new ObjectParameter("phone_number", phone_number) :
                new ObjectParameter("phone_number", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddCustomer", first_nameParameter, last_nameParameter, phone_numberParameter, emailParameter, new_customer_id);
        }
    
        public virtual int AddNewCar(Nullable<int> customerID, string brand, string model, Nullable<int> year, string licensePlate, string vIN, ObjectParameter newCarID)
        {
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            var brandParameter = brand != null ?
                new ObjectParameter("Brand", brand) :
                new ObjectParameter("Brand", typeof(string));
    
            var modelParameter = model != null ?
                new ObjectParameter("Model", model) :
                new ObjectParameter("Model", typeof(string));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var licensePlateParameter = licensePlate != null ?
                new ObjectParameter("LicensePlate", licensePlate) :
                new ObjectParameter("LicensePlate", typeof(string));
    
            var vINParameter = vIN != null ?
                new ObjectParameter("VIN", vIN) :
                new ObjectParameter("VIN", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddNewCar", customerIDParameter, brandParameter, modelParameter, yearParameter, licensePlateParameter, vINParameter, newCarID);
        }
    
        public virtual ObjectResult<GetMechanicsBySpecialization_Result> GetMechanicsBySpecialization(string specialization)
        {
            var specializationParameter = specialization != null ?
                new ObjectParameter("Specialization", specialization) :
                new ObjectParameter("Specialization", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetMechanicsBySpecialization_Result>("GetMechanicsBySpecialization", specializationParameter);
        }
    
        public virtual int GetTotalPaymentsByDateRange(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, ObjectParameter totalAmount)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetTotalPaymentsByDateRange", startDateParameter, endDateParameter, totalAmount);
        }
    
        public virtual ObjectResult<GetWorkOrdersByCustomer_Result> GetWorkOrdersByCustomer(Nullable<int> customerID)
        {
            var customerIDParameter = customerID.HasValue ?
                new ObjectParameter("CustomerID", customerID) :
                new ObjectParameter("CustomerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetWorkOrdersByCustomer_Result>("GetWorkOrdersByCustomer", customerIDParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int UpdateWorkOrderStatus(Nullable<int> work_order_id, string new_status, ObjectParameter rows_updated)
        {
            var work_order_idParameter = work_order_id.HasValue ?
                new ObjectParameter("work_order_id", work_order_id) :
                new ObjectParameter("work_order_id", typeof(int));
    
            var new_statusParameter = new_status != null ?
                new ObjectParameter("new_status", new_status) :
                new ObjectParameter("new_status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateWorkOrderStatus", work_order_idParameter, new_statusParameter, rows_updated);
        }
    
        public virtual int UpdateWorkOrderStatuss(Nullable<int> workOrderID, string newStatus, ObjectParameter oldStatus)
        {
            var workOrderIDParameter = workOrderID.HasValue ?
                new ObjectParameter("WorkOrderID", workOrderID) :
                new ObjectParameter("WorkOrderID", typeof(int));
    
            var newStatusParameter = newStatus != null ?
                new ObjectParameter("NewStatus", newStatus) :
                new ObjectParameter("NewStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateWorkOrderStatuss", workOrderIDParameter, newStatusParameter, oldStatus);
        }
    }
}
