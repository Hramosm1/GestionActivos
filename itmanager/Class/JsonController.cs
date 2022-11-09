using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using itmanager.Models;
using itmanager.Class;
using System.Collections;

namespace itmanager.Class
{
    [Produces("application/json")]
    public class JsonController : Controller
    {
        private itmanagerContext context;

        public JsonController(itmanagerContext _context) {
            context = _context;
        }

        [Route("api/actives")]
        public IActionResult Get() { 

            List<ActActivo> act = context.ActActivos
                 .Include(a => a.Tac)
                 //.Include(a=>a.Tfa)
                 .ToList();

            var array = new ArrayList();
            array.Add(act);

           // var model = new Dictionary<string, ArrayList>();
            var model = new Model();
            model.Draw = 1;
            model["data"] = array;

            var jsonData = new { draw = 1, recordsFiltered = act.Count, recordsTotal = act.Count, data = act };
            return Ok(jsonData);
        }

        [HttpGet]
        [Route("api/employees")]
        public EmpEmpleado GetEmployees()
        {
            EmpEmpleado act = context.EmpEmpleados.FirstOrDefault();
            return act;
        }

        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }

        class Model : Dictionary<string, ArrayList>
        {
            public int Draw { get; set; }
        }

    }
}


namespace System.Data.Entity
{
    using Linq;
    using Linq.Expressions;
    using Text;

    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> source,
          int levelIndex, Expression<Func<TEntity, TEntity>> expression)
        {
            if (levelIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(levelIndex));
            var member = (MemberExpression)expression.Body;
            var property = member.Member.Name;
            var sb = new StringBuilder();
            for (int i = 0; i < levelIndex; i++)
            {
                if (i > 0)
                    sb.Append(Type.Delimiter);
                sb.Append(property);
            }
            return source.Include(sb.ToString());
        }
    }
}
