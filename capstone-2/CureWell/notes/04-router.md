# 04 — navigate and route params

When you need this: Edit buttons, and the update pages that read the URL.

## Syntax — go somewhere

```typescript
this.router.navigate(['/viewDoctors']);
this.router.navigate(['/editDoctorDetails', id, name]);
```

Each array entry after the path is the next `:param` in `app.routing.ts`.

Template link (no TypeScript):

```html
<a routerLink="/viewDoctors">Cancel</a>
```

## Syntax — read params (update pages)

```typescript
ngOnInit() {
  this.doctorId = Number(this.route.snapshot.params['doctorId']);
  this.doctorName = this.route.snapshot.params['doctorName'];
}
```

| Piece | Role |
|---|---|
| `this.route` | injected `ActivatedRoute` |
| `snapshot.params['name']` | the value for `:name` at the moment the page opens |
| `Number(...)` | params arrive as strings; ids and times are numbers in the models |

## Declared paths

```text
editDoctorDetails/:doctorId/:doctorName
editSurgery/:doctorId/:endTime/:startTime/:surgeryCategory/:surgeryDate/:surgeryId
```

View-doctor commit navigates with `doctor.doctorId`, `doctor.doctorName`.

Today’s-surgery commit navigates with:

```typescript
surgery.doctorId, surgery.endTime, surgery.startTime,
surgery.surgeryCategory, surgery.surgeryDate, surgery.surgeryId
```

That order matches the route: **endTime, then startTime**.

## Exam variations

1. `this.route.params.subscribe(p => …)` — same values, updates if the URL changes without destroying the component.
2. Query string `?id=1` is `queryParams`, not `params`. These screens use path params.
3. Swapping start and end in `navigate` fills the wrong fields on the update page, because the route position decides the name.
