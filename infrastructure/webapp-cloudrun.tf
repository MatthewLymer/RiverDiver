resource "google_cloud_run_service" "default" {
  name = "webapp-service"
  location = "us-central1" # cloud-run does not work in Canada yet

  template {
    spec {
      containers {
        image = "gcr.io/matthewlymer-riverdiver/webapp"
        env {
          name = "RD_PORT"
          value = "8080"
        }

        env {
          name = "RD_SENDGRID_KEY"
          value = var.rd_sendgrid_key
        }

        env {
          name = "RD_WAIVER_TO_EMAIL"
          value = var.rd_waiver_to_email
        }

        env {
          name = "RD_WAIVER_FROM_EMAIL"
          value = var.rd_waiver_from_email
        }        
      }
    }
  }

  traffic {
    percent = 100
    latest_revision = true
  }
}

data "google_iam_policy" "noauth" {
  binding {
    role = "roles/run.invoker"
    members = [
      "allUsers",
    ]
  }
}

resource "google_cloud_run_service_iam_policy" "noauth" {
  location = google_cloud_run_service.default.location
  project = google_cloud_run_service.default.project
  service = google_cloud_run_service.default.name
  policy_data = data.google_iam_policy.noauth.policy_data
}

output "url" {
  value = google_cloud_run_service.default.status[0].url
}