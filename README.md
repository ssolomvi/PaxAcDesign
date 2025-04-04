[Radzen documentation](https://blazor.radzen.com/get-started)
[Data flow](https://miro.com/app/board/uXjVIT4Xn-E=/)

[Enum handling in Radzen](https://blazor.radzen.com/dropdown-custom-objects?theme=material3)

[Localization](https://phrase.com/blog/posts/blazor-webassembly-i18n/)
[Generating .resx files in VS](https://randomtutes.wordpress.com/2015/05/18/cannot-retrieve-property-name-because-localization-failed-in-multi-lingual-website-is-soloved/)

todo:
 2. ensure data is binded
 3. implement block1 calculation and block1 results
 4. implement overall logic with fill 1 form -> see results (generated below the form) -> if approved, go to the next step (page n+1), else go edit form
do boolean flags for approving every step calculation?
 5. add navigation with pages

// input form components
```c#
                                    <RadzenRow AlignItems="AlignItems.Center">
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="@Localize["AircraftType"]" Component="AircraftTypeDropDown"/>
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="8">
                                            <RadzenDropDown @bind-value="DataRequest.RequestPurpose.AircraftType"
                                                            TValue="AircraftType"
                                                            AllowClear="true"
                                                            Placeholder="@Localize["SelectAircraftType"]"
                                                            Data="@(Enum.GetValues(typeof(AircraftType)).Cast<Enum>())"
                                                            Style="width: 100%;"
                                                            Name="AircraftTypeDropDown">
                                            </RadzenDropDown>
                                        </RadzenColumn>
                                    </RadzenRow>
                                    <RadzenRow AlignItems="AlignItems.Center">
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="@Localize["HighLiftDevice"]" Component="HighLiftDeviceDropDown"/>
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="8">
                                            <RadzenDropDown @bind-value="DataRequest.RequestPurpose.HighLiftDevice"
                                                            TValue="HighLiftDevice"
                                                            AllowClear="true"
                                                            Placeholder="@Localize["SelectHighLiftDevice"]"
                                                            Data="@(Enum.GetValues(typeof(HighLiftDevice)).Cast<Enum>())"
                                                            Style="width: 100%;"
                                                            Name="HighLiftDeviceDropDown">
                                            </RadzenDropDown>
                                        </RadzenColumn>
                                    </RadzenRow>
                                    <RadzenRow AlignItems="AlignItems.Center">
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="@Localize["HeightAboveSeaLevel"]" Component="HeightAboveSeaLevelNumeric"/>
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="8">
                                            <RadzenNumeric @bind-value="DataRequest.RequestPurpose.HeightAboveSeaLevel"
                                                           TValue="double"
                                                           Format="#.0000"
                                                           Step="0.5"
                                                           Style="width: 100%;"
                                                           Name="HeightAboveSeaLevelNumeric"/>
                                        </RadzenColumn>
                                    </RadzenRow>
                                    <RadzenRow AlignItems="AlignItems.Center">
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="@Localize["LandingDistance"]" Component="LandingDistanceNumeric"/>
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="8">
                                            <RadzenNumeric @bind-value="DataRequest.RequestPurpose.LandingDistance"
                                                           TValue="double"
                                                           Format="#.0000"
                                                           Step="0.5"
                                                           Style="width: 100%;"
                                                           Name="LandingDistanceNumeric"/>
                                        </RadzenColumn>
                                    </RadzenRow>
                                                                        <RadzenRow AlignItems="AlignItems.Center">
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="@Localize["WingAspectRatio"]" Component="WingAspectRatioNumeric"/>
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="8">
                                            <RadzenNumeric @bind-value="DataRequest.RequestPurpose.WingAspectRatio"
                                                           TValue="double"
                                                           Format="#.0000"
                                                           Step="0.5"
                                                           Style="width: 100%;"
                                                           Name="WingAspectRatioNumeric"/>
                                        </RadzenColumn>
                                    </RadzenRow>
                                    <RadzenRow AlignItems="AlignItems.Center">
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="@Localize["WingWettedArea"]" Component="WingWettedAreaNumeric"/>
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="8">
                                            <RadzenNumeric @bind-value="DataRequest.RequestPurpose.WingWettedArea"
                                                           TValue="double"
                                                           Format="#.0000"
                                                           Step="0.5"
                                                           Style="width: 100%;"
                                                           Name="WingWettedAreaNumeric"/>
                                        </RadzenColumn>
                                    </RadzenRow>
```