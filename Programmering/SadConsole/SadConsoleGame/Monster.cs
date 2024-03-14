namespace SadConsoleGame;

internal class Monster : GameObject {

    private double speed;

    public Monster(Point position, IScreenSurface hostingSurface)
    : base(new ColoredGlyph(Color.Red, Color.Black, 'M'), position, hostingSurface) {

        speed = 1;

    }

    public override bool Touched(GameObject source, Map map) {
        return base.Touched(source, map);
    }

    public override bool Update(TimeSpan delta, Map map) {
        System.Console.WriteLine("Raaargh!!");

        var player_pos = map.UserControlledObject.Position;

        var dx = player_pos.X - Position.X;
        var dy = player_pos.Y - Position.Y;

        Point new_pos = new Point(Position.X, Position.Y);
        var nx = Position.X;
        var ny = Position.Y;

        if (Math.Abs(dy) > Math.Abs(dx)) {
            ny += Math.Sign(dy);
        }
        else {
            nx += Math.Sign(dx);
        }

        base.Move(new Point(nx, ny), map);

        return true;
    }
}