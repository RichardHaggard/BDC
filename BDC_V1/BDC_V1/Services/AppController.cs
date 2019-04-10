using BDC_V1.Events;
using BDC_V1.Interfaces;
using Prism.Events;

namespace BDC_V1.Services
{
    public class AppController : IAppController
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

        // **************** Class constructors ********************************************** //

        public AppController()
        {
            EventTypeAggregator.GetEvent<PubSubEvent<string>>().Subscribe(OnUserAction);
        }

        // **************** Class members *************************************************** //


        private static void OnUserAction( string userAction )
        {
            // Normally, arguments from an external agency should be validated before being
            // utilized. This, however, is just proof of concept and need not have extensive
            // error handling built in.

            switch ( userAction )
            {
                case "Login clicked":
                {
                    break;
                }
            }
        }
    }
}
