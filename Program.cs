
namespace ipan;


class Program
{
    static void Main()
    {
        // Set up the global event handlers
        AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

        ServiceRepository repository = new ServiceRepository();

        // Create a service
        Service newService = new Service
        {
            Name = "IPANMUC Service, for client_id 1",
            ClientID = 1,
            OutgoingInvoiceID = 1,
            OperatorID = 1
        };
        repository.CreateService(newService);

        // Get all services
        List<Service> services = repository.GetAllServices();
        foreach (var service in services)
        {
            Console.WriteLine($"ID: {service.ID}, Name: {service.Name}, ClientID: {service.ClientID}, OutgoingInvoiceID: {service.OutgoingInvoiceID}, OperatorID: {service.OperatorID}");
        }

        // Update a service
        Service serviceToUpdate = services[0];
        serviceToUpdate.Name = "Updated Service";
        repository.UpdateService(serviceToUpdate);

        // Delete a service
        int serviceIDToDelete = services[1].ID;
        repository.DeleteService(serviceIDToDelete);


        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            // Handle the exception here, log it, show a message to the user, etc.
#if SHOW_EXCEPTION_STACK
            if ( ex.StackTrace is  not null)
            Console.WriteLine("Stack trace: " + ex.StackTrace.ToString());
#endif

            Console.WriteLine("Exception: " + ex.Message);

            if (ex.InnerException is not null)
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);

            // Gracefully exit the process
            Environment.Exit(1);
        }
    } // run
} // program