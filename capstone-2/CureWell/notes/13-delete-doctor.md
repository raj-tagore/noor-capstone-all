# 13 — Delete doctor

Commit: `DeleteDoctor`. PS: `ps/16` row `removeDoctor`.

## Component

```typescript
removeDoctor(doctor: Doctor) {
  this._curewellService.deleteDoctor(doctor).subscribe({
    next: (response) => {
      this.status = response;
      if (response) {
        alert('Doctor detailed deleted successfully!');
        this.getDoctor();
      } else {
        alert("Doctor's name not deleted");
      }
    },
    error: () => {
      this.errorMsg = 'Some error occured';
    }
  });
}
```

Button: `(click)="removeDoctor(doctor)"`.

| PS | Code |
|---|---|
| pass the doctor into the service | `deleteDoctor(doctor)` |
| `status` | `this.status = response` |
| true → alert | `'Doctor detailed deleted successfully!'` (wording from the table) |
| false → alert | `"Doctor's name not deleted"` |
| error → `errorMsg` | `'Some error occured'` |

`this.getDoctor()` after a successful delete reloads the table. The table does not require it; without it the deleted row stays on screen until refresh.

The service call is in [06](06-service.md): DELETE with the doctor object as the body.
