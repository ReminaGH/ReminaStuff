using System.Numerics;

namespace SadConsoleGame;

internal class Monster : GameObject {

    private double speed;
    private double sub_pos_len;

    public Monster(Point position, IScreenSurface hostingSurface)
    : base(new ColoredGlyph(Color.Red, Color.Black, 'M'), position, hostingSurface) {

        speed = 200f;
        sub_pos_len = 0.0f;
    }

    public override bool Touched(GameObject source, Map map) {
        return base.Touched(source, map);
    }

    public override bool Update(TimeSpan delta, Map map) {

        var player_pos = map.UserControlledObject.Position;

        sub_pos_len += speed * delta.TotalSeconds;

        var dx = (player_pos.X - Position.X);
        var dy = (player_pos.Y - Position.Y);

        if (sub_pos_len > 100) {

            sub_pos_len = 0.0;

            Point new_pos = new Point(Position.X, Position.Y);
            var nx = Position.X;
            var ny = Position.Y;

            var distance = Math.Sqrt(dx * dx + dy * dy);

            if (Math.Abs(dy) > Math.Abs(dx)) {
                ny += Math.Sign(dy);
            }
            else {
                nx += Math.Sign(dx);
            }

            base.Move(new Point(nx, ny), map);
        }

        return true;
    }
}