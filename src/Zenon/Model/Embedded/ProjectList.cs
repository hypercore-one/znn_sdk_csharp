using System.Linq;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class ProjectList
    {
        public long Count { get; }
        public Project[] List { get; }

        public ProjectList(Json.JProjectList json)
        {
            Count = json.count;
            List = json.list.Select(x => new Project(x)).ToArray();
        }

        public virtual Json.JProjectList ToJson()
        {
            return new Json.JProjectList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }

        public Project FindId(Hash id)
        {
            return List.FirstOrDefault(x => x.Id == id);
        }

        public Project FindProjectByPhaseId(Hash id)
        {
            for (var i = 0; i < List.Length; i++)
            {
                for (var j = 0; j < List[i].PhaseIds.Length; i++)
                {
                    if (id == List[i].PhaseIds[j])
                        return List[i];
                }
            }
            return null;
        }
    }
}
