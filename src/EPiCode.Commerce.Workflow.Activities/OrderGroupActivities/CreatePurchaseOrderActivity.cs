using System.Workflow.ComponentModel;

namespace EPiCode.Commerce.Workflow.Activities
{
    public partial class CreatePurchaseOrderActivity : OrderGroupActivityBase
    {
        public CreatePurchaseOrderActivity()
        {
            InitializeComponent();
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            // Validate the properties at runtime
            this.ValidateRuntime();

              var cart = OrderGroup as Mediachase.Commerce.Orders.Cart;

            if (cart != null)
            {
                var purchaseOrder = cart.SaveAsPurchaseOrder();

                cart.Delete();
                cart.AcceptChanges();
                OrderGroup = purchaseOrder;
            }

            return ActivityExecutionStatus.Closed;
        }
    }
}
