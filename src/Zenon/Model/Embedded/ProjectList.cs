using System.Linq;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class ProjectList : IJsonConvertible<JProjectList>
    {
        public long Count { get; }
        public Project[] List { get; }

        public ProjectList(JProjectList json)
        {
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new Project(x)).ToArray()
                : new Project[0];
        }

        public virtual JProjectList ToJson()
        {
            return new JProjectList()
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
