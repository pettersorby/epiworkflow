using System.Workflow.ComponentModel;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Managers;

namespace EPiCode.Commerce.Workflow.Activities
{
	public partial class CalculateExchangeOrderStatusActivity : ReturnFormBaseActivity
	{
		public CalculateExchangeOrderStatusActivity()
		{
			InitializeComponent();
		}

		protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
		{
			try
			{
				if (base.ReturnFormStatus == ReturnFormStatus.Complete)
				{
					//Need change ExchangeOrder from AvaitingCompletition to InProgress
					var exchangeOrder = ReturnExchangeManager.GetExchangeOrderForReturnForm(base.ReturnOrderForm);
					if (exchangeOrder != null && OrderStatusManager.GetOrderGroupStatus(exchangeOrder) == OrderStatus.AwaitingExchange)
					{
						OrderStatusManager.ProcessOrder(exchangeOrder);
						exchangeOrder.AcceptChanges();
					}
				}
				// Retun the closed status indicating that this activity is complete.
				return ActivityExecutionStatus.Closed;
			}
			catch
			{
				// An unhandled exception occured.  Throw it back to the WorkflowRuntime.
				throw;
			}
		}
	}
}
