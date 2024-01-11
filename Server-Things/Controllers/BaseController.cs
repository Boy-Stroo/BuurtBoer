using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Things;

public class BaseController : ControllerBase
{
    protected readonly BuurtboerContext db;
    public BaseController()
    {
        db = new BuurtboerContext();
    }

    public BaseController(BuurtboerContext db)
    {
        this.db = db;
    }
}