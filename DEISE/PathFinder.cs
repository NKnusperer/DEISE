using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DEISE
{
    internal class PathFinder
    {
        internal static PathGeometry GetPathGeometry(ConnectorInfo source, ConnectorInfo sink)
        {
            var rectSource = GetRectWithMargin(source, 0);
            var rectSink = GetRectWithMargin(sink, 13);

            var startPoint = GetOffsetPoint(source, rectSource);
            var endPoint = GetOffsetPoint(sink, rectSink);

            var midpoint = CalculateMidpoint(startPoint, new Point(endPoint.X, startPoint.Y));
            var distance = CalculateDistance(startPoint, midpoint);

            var p1 = CalculateEndpoint(startPoint, distance, GetAngel(source.Orientation));
            var p2 = CalculateEndpoint(endPoint, distance, GetAngel(sink.Orientation));
            var p3 = endPoint;

            var geometry = new PathGeometry();

            var pathFigure = new PathFigure();
            pathFigure.StartPoint = startPoint;
            geometry.Figures.Add(pathFigure);

            var bs = new BezierSegment(p1, p2, p3, true);
            pathFigure.Segments.Add(bs);

            return geometry;
        }

        internal static PathGeometry GetPathGeometry(ConnectorInfo source, Point sinkPoint, ConnectorOrientation preferredOrientation)
        {
            var rectSource = GetRectWithMargin(source, 0);
            var startPoint = GetOffsetPoint(source, rectSource);
            var endPoint = sinkPoint;

            var midpoint = CalculateMidpoint(startPoint, new Point(endPoint.X, startPoint.Y));
            var distance = CalculateDistance(startPoint, midpoint);

            var p1 = CalculateEndpoint(startPoint, distance, GetAngel(source.Orientation));
            var p2 = CalculateEndpoint(endPoint, distance, GetAngel(preferredOrientation));
            var p3 = endPoint;

            var geometry = new PathGeometry();

            var pathFigure = new PathFigure();
            pathFigure.StartPoint = startPoint;
            geometry.Figures.Add(pathFigure);

            var bs = new BezierSegment(p1, p2, p3, true);
            pathFigure.Segments.Add(bs);

            return geometry;
        }

        private static double GetAngel(ConnectorOrientation orientation)
        {
            switch (orientation)
            {
                case ConnectorOrientation.Left:
                    return 180;
                case ConnectorOrientation.Top:
                    return 270;
                case ConnectorOrientation.Right:
                    return 0;
                case ConnectorOrientation.Bottom:
                    return 90;
                default:
                    throw new Exception("Unknown ConnectorOrientation");

            }

        }

        private static Point CalculateEndpoint(Point startPoint, double length, double angel)
        {
            var radians = angel * Math.PI / 180;
            return new Point(startPoint.X + length * Math.Cos(radians), startPoint.Y + length * Math.Sin(radians));
        }

        private static Point CalculateMidpoint(Point p1, Point p2)
        {
            return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        private static double CalculateDistance(Point p1, Point p2)
        {
            return Point.Subtract(p1, p2).Length;
        }

        private static Rect GetRectWithMargin(ConnectorInfo connectorThumb, double margin)
        {
            Rect rect = new Rect(connectorThumb.DesignerItemLeft,
                                 connectorThumb.DesignerItemTop,
                                 connectorThumb.DesignerItemSize.Width,
                                 connectorThumb.DesignerItemSize.Height);

            rect.Inflate(margin, margin);

            return rect;
        }

        private static Point GetOffsetPoint(ConnectorInfo connector, Rect rect)
        {
            Point offsetPoint = new Point();

            switch (connector.Orientation)
            {
                case ConnectorOrientation.Left:
                    offsetPoint = new Point(rect.Left, connector.Position.Y);
                    break;
                case ConnectorOrientation.Top:
                    offsetPoint = new Point(connector.Position.X, rect.Top);
                    break;
                case ConnectorOrientation.Right:
                    offsetPoint = new Point(rect.Right, connector.Position.Y);
                    break;
                case ConnectorOrientation.Bottom:
                    offsetPoint = new Point(connector.Position.X, rect.Bottom);
                    break;
                default:
                    break;
            }

            return offsetPoint;
        }

        public static ConnectorOrientation GetOpositeOrientation(ConnectorOrientation connectorOrientation)
        {
            switch (connectorOrientation)
            {
                case ConnectorOrientation.Left:
                    return ConnectorOrientation.Right;
                case ConnectorOrientation.Top:
                    return ConnectorOrientation.Bottom;
                case ConnectorOrientation.Right:
                    return ConnectorOrientation.Left;
                case ConnectorOrientation.Bottom:
                    return ConnectorOrientation.Top;
                default:
                    return ConnectorOrientation.Top;
            }
        }
    }
}
