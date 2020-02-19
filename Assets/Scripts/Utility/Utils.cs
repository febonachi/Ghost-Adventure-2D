using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public enum ItemOrder { None, First, Second, Third, Key };

[Serializable]
public class UnityEventInt : UnityEvent<int> { }

[Serializable]
public class UnityEventFloat : UnityEvent<float> { }

[Serializable]
public class UnityEventItemOrder : UnityEvent<ItemOrder> { }

public class TwoStateValue<T> {
    public T first;
    public T second;
}

[Serializable]
public class TwoStateInt : TwoStateValue<int> { }

[Serializable]
public class TwoStateFloat : TwoStateValue<float> { }

[Serializable]
public class TwoStateColor : TwoStateValue<Color> { }

[Serializable]
public class TwoStateVector2 : TwoStateValue<Vector2> { }

[Serializable]
public class TwoStateParticleSystem : TwoStateValue<ParticleSystem> { }

[Serializable]
public class Size2d {
    public float width = 1f;
    public float height = 1f;
}

public static class Utils {

    public static Color transparentWhite => new Color(1f, 1f, 1f, 0f);
    public static Color transparentBlack => new Color(0f, 0f, 0f, 0f);

    public static float Percent(float value, float max) {
        return (value * 100f) / max;
    }

    public static float TakePercent(float value, float percent) {
        return (value * percent) / 100f;
    }

    public static float Random(float min = 0f, float max = 1f) {
        return UnityEngine.Random.Range(min, max);
    }

    public static int Random(int min = 0, int max = 100) {
        return UnityEngine.Random.Range(min, max);
    }

    public static Vector3 GetPointInCollider(GameObject obj) {
        Vector3 result = Vector3.zero;
        Bounds bounds = obj.GetComponent<Collider2D>().bounds;
        result.x = Random(bounds.min.x, bounds.max.x);
        result.y = Random(bounds.min.y, bounds.max.y);
        return result;
    }

    public static IEnumerator ColorOverLifeTime(SpriteRenderer sr, Color color, float time) {
        float elapsed = 0f;
        while(elapsed < time) {
            float smooth = elapsed / time;
            sr.color = Color.Lerp(sr.color, color, smooth);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = color;
    }

    public static IEnumerator ColorOverLifeTime(Image sr, Color color, float time) {
        float elapsed = 0f;
        while (elapsed < time) {
            float smooth = elapsed / time;
            sr.color = Color.Lerp(sr.color, color, smooth);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = color;
    }

    public static IEnumerator ScaleOverLifeTime(Transform t, Vector3 scale, float time) {
        float elapsed = 0f;
        while (elapsed < time) {
            float smooth = elapsed / time;
            t.localScale = Vector3.Lerp(t.localScale, scale, smooth);
            elapsed += Time.deltaTime;
            yield return null;
        }
        t.localScale = scale;
    }

    public static IEnumerator MoveOverLifeTime(Transform t, Vector2 pos, float time) {
        float elapsed = 0f;
        while (elapsed < time) {
            float smooth = elapsed / time;
            t.position = Vector2.MoveTowards(t.position, pos, smooth);
            elapsed += Time.deltaTime;
            yield return null;
        }
        t.position = pos;
    }

    public static IEnumerator RotateOverLifeTime(Transform t, Vector3 direction, float angle, float speed) {
        float a = 0f;
        while (a < Mathf.Abs(angle)) {
            t.Rotate(direction * speed);
            a += speed;
            yield return null;
        }
        t.Rotate(-direction, a - Mathf.Abs(angle));
    }

    public static IEnumerator DissolveOverLifeTime(SpriteRenderer sr) {
        Material mat = sr.material;
        float progress = 1f;
        while (progress > 0.05f) {
            progress = Mathf.Lerp(progress, 0f, Time.deltaTime);
            mat.SetFloat("_Progress", progress);
            yield return null;
        }
        mat.SetFloat("_Progress", 0f);
    }

    public static IEnumerator RessolveOverLifeTime(SpriteRenderer sr) {
        Material mat = sr.material;
        float progress = 0f;
        while (progress < 0.95f) {
            progress = Mathf.Lerp(progress, 1f, Time.deltaTime);
            mat.SetFloat("_Progress", progress);
            yield return null;
        }
        mat.SetFloat("_Progress", 1f);
    }

    public static void RotateTo(Transform t, Transform to) {
        Vector2 direction = t.position - to.position;
        direction.Normalize();
        t.up = direction;
    }

    public static void RotateTo(Transform t, Vector3 to) {
        Vector2 direction = t.position - to;
        direction.Normalize();
        t.up = -direction;
    }

    public static void Hide<T>(T[] objects) where T : Component {
        foreach(T obj in objects) {
            obj.gameObject.SetActive(false);
        }
    }

    public static void Show<T>(T[] objects) where T : Component {
        foreach (T obj in objects) {
            obj.gameObject.SetActive(true);
        }
    }
}
