using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerShotTrajectory : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer;
        [SerializeField]
        private LayerMask layerMask;
        [Inject]
        private CoreSettings coreSettings;
        [Inject]
        private IPlayerControl playerControl;

        public void Init()
        {
            HideTrajectory();

            playerControl.onControlStart += ShowTrajectory;
            playerControl.onControlUpdate += UpdateTrajectory;
            playerControl.onControlEnd += (v) => HideTrajectory();
        }

        public void ShowTrajectory(Vector2 direction)
        {
            UpdateTrajectory(direction);
            lineRenderer.gameObject.SetActive(true);
        }

        private void HideTrajectory()
        {
            lineRenderer.gameObject.SetActive(false);
        }

        private void UpdateTrajectory(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Ray ray = new Ray(lineRenderer.transform.position, lineRenderer.transform.right);
            float remainingDistance = coreSettings.playerShotSettings.trajectoryDistance;
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, ray.origin);

            Collider2D lastHittedCollider = null;

            do
            {
                var hit = Physics2D.Raycast(ray.origin, ray.direction, remainingDistance, layerMask);

                if (hit.collider != null)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                    remainingDistance -= hit.distance;
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                    if (lastHittedCollider != null) lastHittedCollider.enabled = true;

                    lastHittedCollider = hit.collider;
                    lastHittedCollider.enabled = false;
                }
                else
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.GetPoint(remainingDistance));
                    remainingDistance = 0f;

                    if (lastHittedCollider != null) lastHittedCollider.enabled = true;
                }
            } while (remainingDistance > 0f);
        }
    }
}
