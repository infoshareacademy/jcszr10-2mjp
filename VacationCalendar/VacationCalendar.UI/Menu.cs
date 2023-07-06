
namespace VacationCalendar.UI
{
    internal class Menu
    {
        public Menu(IEnumerable<string> items)
        {
            Items = items.ToArray();
        }
        public IReadOnlyList<string> Items { get; }
        public int SelectedIndex { get; private set; } = -1; // nothing selected
        public string SelectedOption => SelectedIndex != -1 ? Items[SelectedIndex] : null;
        public void MoveUp() => SelectedIndex = Math.Max(SelectedIndex - 1, 0);
        public void MoveDown() => SelectedIndex = Math.Min(SelectedIndex + 1, Items.Count - 1);
    }
}
