Create Function [dbo].[fnGetDistance]
(
      @Lat1 Float(18), 
      @Long1 Float(18),
      @Lat2 Float(18),
      @Long2 Float(18),
      @ReturnType VarChar(10)
)

Returns Float(18)

AS

Begin

      Declare @R Float(8);
      Declare @dLat Float(18);
      Declare @dLon Float(18);
      Declare @a Float(18);
      Declare @c Float(18);
      Declare @d Float(18);

      Set @R = 
            Case @ReturnType 
            When 'Miles' Then 3956.55 
            When 'Kilometers' Then 6367.45
            When 'Feet' Then 20890584
            When 'Meters' Then 6367450
            Else 6367450 -- Default value
            End

      Set @dLat = Radians(@lat2 - @lat1);

      Set @dLon = Radians(@long2 - @long1);

      Set @a = Sin(@dLat / 2) 
                 * Sin(@dLat / 2) 
                 + Cos(Radians(@lat1))
                 * Cos(Radians(@lat2)) 
                 * Sin(@dLon / 2) 
                 * Sin(@dLon / 2);
      Set @c = 2 * Asin(Min(Sqrt(@a)));

      Set @d = @R * @c;
      Return @d;

End 