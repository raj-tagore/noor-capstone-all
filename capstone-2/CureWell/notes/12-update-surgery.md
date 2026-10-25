# 12 — Update surgery

Commit: `UpdateSurgery`. PS: `ps/11`.

## ngOnInit — names must match the route

Route:

```text
editSurgery/:doctorId/:endTime/:startTime/:surgeryCategory/:surgeryDate/:surgeryId
```

```typescript
this.surgeryId = Number(this.route.snapshot.params['surgeryId']);
this.surgeryCategory = this.route.snapshot.params['surgeryCategory'];
this.surgeryDate = this.route.snapshot.params['surgeryDate'];
this.startTime = Number(this.route.snapshot.params['startTime']);
this.endTime = Number(this.route.snapshot.params['endTime']);
this.doctorId = Number(this.route.snapshot.params['doctorId']);
```

`params['startTime']` is correct even though start time is the third segment. The name comes from the route declaration, not from left-to-right counting in this method.

## editSurgery

The service signature is `(doctorId, endTime, startTime, surgeryCategory, surgeryDate, surgeryId)`:

```typescript
editSurgery(startTime: number, endTime: number) {
  this._cureWellService.editSurgery(
    this.doctorId,
    endTime,
    startTime,
    this.surgeryCategory,
    this.surgeryDate,
    this.surgeryId
  ).subscribe({
    next: (response) => {
      this.status = response;
      if (response) { alert('Surgery details updated successfully!'); }
      else { alert('Surgery details not updated'); }
      this.router.navigate(['/viewTodaySurgery']);
    },
    error: (error) => {
      this.errorMsg = error;
      alert('Some error occurred');
      this.router.navigate(['/viewTodaySurgery']);
    },
    complete: () => {
      console.log('Updated surgery details successfully');
    }
  });
}
```

HTML calls `(ngSubmit)="editSurgery(startTime, endTime)"`. Doctor id, date, and category inputs are `readonly`. Time rules are in [05](05-template-forms.md).

## Exam variations

1. Calling `editSurgery(doctorId, startTime, endTime, …)` — wrong: the second argument is **end** time.
2. `Number` on `surgeryDate` — leave the date as the route string.
