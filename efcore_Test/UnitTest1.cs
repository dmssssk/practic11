using System.Runtime.InteropServices.JavaScript;

namespace efcore_Test;
using Arch.EFCore;
using Xunit.Priority;


public class DataTest
{

    
    public static IEnumerable<object[]> DataCreate() =>
    [
        ["qwerty", DateTimeOffset.Now],
        ["sdaghkfgdskkkhjkkfgkkksdhjkf", DateTimeOffset.UnixEpoch],
        ["првиеть пвьлпдьтадвыльа аываыа ываы ава", DateTimeOffset.MaxValue]
    ];

    public static IEnumerable<object[]> DataReadTextsSuccess() =>
    [
        ["qwerty"],
        ["аыввыа"],
        ["kkk"]
    ];
    
    public static IEnumerable<object[]> DataReadTextsFail() =>
    [
        ["kmlnmkbccxx oopy erwrw   ;.; "],
        ["првие мир"],
        ["hello world"]
    ];
    
    public static IEnumerable<object[]> DataReadIdsFail() =>
    [
        [11111],
        [222222],
        [324242]
    ];



    public static IEnumerable<object[]> UpdateData() => 
    [
        ["qweqwe", DateTimeOffset.Now],
        ["qwcvbvcmjhjmhkhkhj", DateTimeOffset.Now],
        ["qweqwdcvbbcnuykyuir4ertzdfggs453456743dfhe", DateTimeOffset.Now]
    ];

}

[TestCaseOrderer("Xunit.Priority.PriorityOrderer", "Xunit.Priority")]
public class UnitTest1 : IAsyncLifetime
{
    
    public async Task InitializeAsync()
    {
        await using var db = new DataContext();
        await db.Database.EnsureCreatedAsync();
    }
    
    public async Task DisposeAsync()
    {
        await Task.CompletedTask;
    }
    
    [Theory, Priority(1)]
    [MemberData(nameof(DataTest.DataCreate), MemberType = typeof(DataTest))]
    public async Task CreateSuccess(string text, DateTimeOffset date)
    {
        Assert.IsType<Note>(await Crud.Create(text, date));
    }

    [Theory, Priority(2)]
    [MemberData(nameof(DataTest.DataReadTextsSuccess), MemberType = typeof(DataTest))]
    public async Task FindTextsSuccess(string text)
    {
        Assert.IsType<List<Note>>(await Crud.Read(text));
    }
    
    [Theory, Priority(2)]
    [MemberData(nameof(DataTest.DataReadTextsFail), MemberType = typeof(DataTest))]
    public async Task FindTextsFailed(string text)
    {
        Assert.IsType<List<Note>>(await Crud.Read(text));
    }
    
    
    [Theory, Priority(2)]
    [MemberData(nameof(DataTest.DataReadIdsFail), MemberType = typeof(DataTest))]
    public async Task FindIdFailed(int id)
    {
        Assert.Null(await Crud.Read(id));
    }
    
    
    
    
    [Theory, Priority(3)]
    [MemberData(nameof(DataTest.UpdateData), MemberType = typeof(DataTest))]
    public async Task UpdateSuccess(string text, DateTimeOffset date)
    {
        var note = await Crud.Create("qwerty", DateTimeOffset.Now);
        
        await Crud.Update(note, text, date);
    }
    
    
    [Fact, Priority(4)]
    public async Task DeleteSuccess()
    {
        var note = await Crud.Create("qwerty", DateTimeOffset.Now);
        
        await Crud.Delete(note);
    }
    
}