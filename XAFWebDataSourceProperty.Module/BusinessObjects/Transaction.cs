using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XAFWebDataSourceProperty.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Transaction : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Transaction(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }



        CriteriaOperator criteriaProperty;
        Invoice invoice;
        Customer customer;
        double amount;
        string transactionName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string TransactionName
        {
            get => transactionName;
            set => SetPropertyValue(nameof(TransactionName), ref transactionName, value);
        }


        public double Amount
        {
            get => amount;
            set => SetPropertyValue(nameof(Amount), ref amount, value);
        }



        [Association("Customer-Transactions")]
        [ImmediatePostData]
        public Customer Customer
        {
            get => customer;
            set => SetPropertyValue(nameof(Customer), ref customer, value);
        }


        [Association("Invoice-Transactions")]
        [DataSourceCriteriaProperty(nameof(CriteriaProperty))]
        public Invoice Invoice
        {
            get => invoice;
            set => SetPropertyValue(nameof(Invoice), ref invoice, value);
        }

        
        public CriteriaOperator CriteriaProperty
        {
            get 
            {
                if (Customer == null) { return CriteriaOperator.Parse("true"); }

                else
                {
                  

                   return CriteriaOperator.FromLambda<Invoice>(i => i.Customer.Oid == Customer.Oid && i.Open);
                }
            
            }
           
        }
    }
}