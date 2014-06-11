using System.Linq;
using System.Reflection;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using EPiServer.BaseLibrary.Search;
using EPiServer.ServiceLocation;
using log4net;
using Riccovero.Core.Services;

namespace EPiCode.Commerce.Workflow.Activities
{
    public partial class SendReceipt : OrderGroupActivityBase
    {
        protected static ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SendReceipt()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called by the workflow runtime to execute an activity.
        /// </summary>
        /// <param name="executionContext">The <see cref="T:System.Workflow.ComponentModel.ActivityExecutionContext"/> to associate with this <see cref="T:System.Workflow.ComponentModel.Activity"/> and execution.</param>
        /// <returns>
        /// The <see cref="T:System.Workflow.ComponentModel.ActivityExecutionStatus"/> of the run task, which determines whether the activity remains in the executing state, or transitions to the closed state.
        /// </returns>
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            try
            {
                // Validate the properties at runtime
                this.ValidateRuntime();

                var mailService = ServiceLocator.Current.GetInstance<IMailService>();
                mailService.SendReceipt(OrderGroup.OrderGroupId);


                _log.Debug("Sending receipt e-mail");

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
