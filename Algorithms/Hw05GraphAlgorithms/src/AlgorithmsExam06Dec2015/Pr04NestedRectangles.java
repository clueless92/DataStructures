package AlgorithmsExam06Dec2015;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

public class Pr04NestedRectangles {
    private static class Rectangle {
        String name;
        int x1;
        int y1;
        int x2;
        int y2;
        int bestDepth = 0;
        Rectangle bestRect;

        Rectangle(String name, int x1, int y1, int x2, int y2) {
            this.name = name;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        @Override
        public String toString() {
            return this.name;
        }
    }

    private static final List<Rectangle> rects = new ArrayList<>();

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        while (true) {
            String line = reader.readLine();
            if (line.equals("End")) {
                break;
            }
            String[] parts = line.split(" ");
            Rectangle rect = new Rectangle(
                    parts[0].substring(0, parts[0].length() - 1),
                    Integer.parseInt(parts[1]),
                    Integer.parseInt(parts[2]),
                    Integer.parseInt(parts[3]),
                    Integer.parseInt(parts[4])
            );
            rects.add(rect);
        }

        Rectangle best = rects.get(0);
        for (int i = 0; i < rects.size(); i++) {
            Rectangle rect = rects.get(i);
            findNestedRects(rect);
            if (rect.bestDepth > best.bestDepth ||
                    rect.bestDepth == best.bestDepth && rect.name.compareTo(best.name) < 0) {
                best = rect;
            }
        }

        StringBuilder output = new StringBuilder();
        while (best != null) {
            output.append(best).append(" < ");
            best = best.bestRect;
        }
        output.setLength(output.length() - 3);
        System.out.println(output);
    }

    private static void findNestedRects(Rectangle rectangle) {
        if (rectangle.bestDepth > 0) {
            return;
        }

        Rectangle bestRect = null;
        rectangle.bestDepth = 1;
        for (int i = 0; i < rects.size(); i++) {
            Rectangle other = rects.get(i);
            if (isInside(other, rectangle) && other != rectangle) {
                findNestedRects(other);
                if (bestRect == null ||
                        other.bestDepth > bestRect.bestDepth ||
                        other.bestDepth == bestRect.bestDepth && other.name.compareTo(bestRect.name) < 0) {
                    bestRect = other;
                }
            }
        }

        if (bestRect != null) {
            rectangle.bestDepth = bestRect.bestDepth + 1;
            rectangle.bestRect = bestRect;
        }
    }

    private static boolean isInside(Rectangle innerRect, Rectangle outerRect) {
        return innerRect.x1 >= outerRect.x1 && innerRect.x1 <= outerRect.x2 &&
                innerRect.x2 >= outerRect.x1 && innerRect.x2 <= outerRect.x2 &&
                innerRect.y1 >= outerRect.y2 && innerRect.y1 <= outerRect.y1 &&
                innerRect.y2 >= outerRect.y2 && innerRect.y2 <= outerRect.y1;
    }
}
