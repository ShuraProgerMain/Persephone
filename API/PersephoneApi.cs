using CARDINAL.Persephone.API.Interfaces;
using CARDINAL.Persephone.Interfaces;
using Console = CARDINAL.Persephone.Helpers.Console;

namespace CARDINAL.Persephone.API;

public class PersephoneApi : IPersephoneApi
{
    private IContext? _context;

    private IContext? Context
    {
        get
        {
            if (_context == null)
            {
                Console.LogError("Persephone don't initialized");
                throw new Exception();
            }
            
            return _context;
        }
    }

    public async Task Initialize()
    {
        _context = new Context();
        await _context.InitContext();
    }

    public ILogsCollectController GetLogsCollector()
    {
        return Context!.LogsCollectController;
    }

    public ILogsFileGenerateController GetLogsFileGenerator()
    {
        return Context!.LogsFileGenerateController;
    }
}