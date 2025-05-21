using Colleagues.Interfaces;
using Colleagues.Models;

public class CollaborationCalculator : ICollaborationCalculator
{
    public List<CommonProjectEmployee> CalculateTotalDaysPerPair(List<EmployeeProjectInfo> entries)
    {
        var pairWorkTracker = new Dictionary<(int, int), int>();
        var result = new List<CommonProjectEmployee>();

        var projects = entries.GroupBy(e => e.ProjectID);

        foreach (var projectGroup in projects)
        {
            var employees = projectGroup.ToList();

            foreach (var (empA, empB) in GetEmployeePairs(employees))
            {
                if (TryCalculateOverlap(empA, empB, out int overlapDays))
                {
                    var key = OrderPair(empA.EmpID, empB.EmpID);
                    if (!pairWorkTracker.ContainsKey(key))
                        pairWorkTracker[key] = 0;

                    pairWorkTracker[key] += overlapDays;

                    result.Add(new CommonProjectEmployee
                    {
                        EmployeeId1 = key.Item1,
                        EmployeeId2 = key.Item2,
                        ProjectId = empA.ProjectID,
                        DaysWorked = overlapDays
                    });
                }
            }
        }

        return result.OrderByDescending(r => r.DaysWorked).ToList();
    }

    public List<CommonProjectEmployee> CalculateDaysPerPairPerProject(List<EmployeeProjectInfo> entries)
    {
        var result = new List<CommonProjectEmployee>();
        var projects = entries.GroupBy(e => e.ProjectID);

        foreach (var projectGroup in projects)
        {
            var employees = projectGroup.ToList();

            foreach (var (empA, empB) in GetEmployeePairs(employees))
            {
                if (TryCalculateOverlap(empA, empB, out int overlapDays))
                {
                    var key = OrderPair(empA.EmpID, empB.EmpID);

                    result.Add(new CommonProjectEmployee
                    {
                        EmployeeId1 = key.Item1,
                        EmployeeId2 = key.Item2,
                        ProjectId = empA.ProjectID,
                        DaysWorked = overlapDays
                    });
                }
            }
        }

        return result;
    }

    public List<CommonProjectEmployee> GetTopPairWithProjects(List<EmployeeProjectInfo> entries)
    {
        var allResults = CalculateTotalDaysPerPair(entries);

        var topPairGroup = allResults
            .GroupBy(r => (r.EmployeeId1, r.EmployeeId2))
            .OrderByDescending(g => g.Sum(x => x.DaysWorked))
            .FirstOrDefault();

        return topPairGroup?.ToList() ?? new List<CommonProjectEmployee>();
    }

    #region Helper

    private static IEnumerable<(EmployeeProjectInfo, EmployeeProjectInfo)> GetEmployeePairs(List<EmployeeProjectInfo> employees)
    {
        for (int i = 0; i < employees.Count; i++)
        {
            for (int j = i + 1; j < employees.Count; j++)
            {
                yield return (employees[i], employees[j]);
            }
        }
    }

    private static bool TryCalculateOverlap(EmployeeProjectInfo empA, EmployeeProjectInfo empB, out int overlapDays)
    {
        var overlapStart = empA.DateFrom > empB.DateFrom ? empA.DateFrom : empB.DateFrom;
        var overlapEnd = empA.DateTo < empB.DateTo ? empA.DateTo : empB.DateTo;

        if (overlapStart <= overlapEnd)
        {
            overlapDays = (overlapEnd - overlapStart).Days + 1;
            return true;
        }

        overlapDays = 0;
        return false;
    }

    private static (int, int) OrderPair(int id1, int id2)
    {
        return id1 < id2 ? (id1, id2) : (id2, id1);
    }

    #endregion
}
