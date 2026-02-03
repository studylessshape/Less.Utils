# Less.Utils.WPF

## IndexOfSelfConverter

[IndexOfSelfConverter](./Converters/IndexOfSelfConverter.cs)

Usage:

```csharp
<ItemsControl
    x:Name="screwOrderCtrl"
    d:ItemsSource="{d:SampleData}"
    DockPanel.Dock="Right">
    <ItemsControl.ItemsPanel>
        <ItemsPanelTemplate>
            <StackPanel Orientation="Horizontal" />
        </ItemsPanelTemplate>
    </ItemsControl.ItemsPanel>
    <ItemsControl.ItemTemplate>
        <DataTemplate>
            <StackPanel Orientation="Horizontal">
                <Label
                    Content="{Binding RelativeSource={RelativeSource Mode=TemplatedParent}, Converter={StaticResource TemplateIndexInItemsControlConverterAppend1}}"
                    ContentStringFormat="{}Index{0}: "/>
                <TextBlock
                    Margin="0,0,10,0"
                    Text="{Binding RelativeSource={RelativeSource Mode=TemplatedParent}, Converter={StaticResource TemplateIndexInItemsControlConverter}}">
                </TextBlock>
            </StackPanel>
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>
```