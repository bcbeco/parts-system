# Parts System – Telepítési útmutató (Kubernetes + ArgoCD)

## 1. Előfeltételek

A rendszer futtatásához az alábbi eszközök szükségesek:

* Docker Desktop (Kubernetes engedélyezve)
* kubectl
* Helm
* Git

---

## 2. Kubernetes klaszter indítása

Docker Desktop esetén:

* Settings → Kubernetes → Enable Kubernetes

Ellenőrzés:

kubectl get nodes

---

## 3. MongoDB telepítése Helm chart-tal

Helm repository hozzáadása:

helm repo add bitnami https://charts.bitnami.com/bitnami

helm repo update

MongoDB telepítése:

helm install mongo bitnami/mongodb 
--set auth.enabled=false

Ellenőrzés:

kubectl get pods

---

## 4. ArgoCD telepítése

Argo CD

Namespace létrehozása:

kubectl create namespace argocd

ArgoCD telepítése:

kubectl apply -n argocd -f https://raw.githubusercontent.com/argoproj/argo-cd/stable/manifests/install.yaml

---

## 5. ArgoCD elérése

Port-forward:

kubectl port-forward svc/argocd-server -n argocd 8080:443

Böngésző:

https://localhost:8080

Felhasználónév:

admin

admin jelszó lekérdezése (windows): 
 
$pass = kubectl get secret argocd-initial-admin-secret -n argocd -o jsonpath="{.data.password}"
[System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($pass)) 

admin jelszó lekérdezése (linux):

kubectl get secret argocd-initial-admin-secret -n argocd -o jsonpath="{.data.password}" | base64 -d

---

## 6. Git repository előkészítése

A repository tartalmazza:

* backend (ASP.NET)
* frontend (Angular)
* k8s/ mappa (deployment YAML-ek)

---

## 7. ArgoCD Application létrehozása

ArgoCD UI → New App

Beállítások:

* Application Name: parts-app
* Project: default
* Repository URL: (GitHub repo URL)
* Path: k8s
* Cluster: in-cluster
* Namespace: default

Sync policy:

* Auto Sync: enabled
* Self Heal: enabled
* Prune: enabled

---

## 8. Automatikus deploy működése

A rendszer GitOps elven működik:

GitHub Actions

Folyamat:

1. Kód módosítása → Git push
2. CI build → Docker image készül
3. Image feltöltés → GitHub Container Registry
4. ArgoCD észleli a változást
5. Kubernetes automatikusan frissül

---

## 9. Alkalmazás elérése

Ingress vagy port-forward használható.

Port-forward példa:

kubectl port-forward svc/parts-frontend-service 4200:80

Elérés:

http://localhost:4200

---

## 10. Összefoglalás

A rendszer komponensei:

* Frontend: Angular
* Backend: ASP.NET
* Adatbázis: MongoDB (Helm chart)
* CI: GitHub Actions
* CD: ArgoCD
* Platform: Kubernetes

A rendszer teljesen automatizált, GitOps alapú telepítéssel működik.
