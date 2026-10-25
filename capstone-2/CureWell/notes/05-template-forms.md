# 05 — Template forms (validation text)

When you need this: add-doctor, update-doctor, and update-surgery HTML. The PS names the exact message and when it appears.

## Syntax

```html
<form #formRef="ngForm" (ngSubmit)="method(field)">
  <input name="doctorName" required [(ngModel)]="doctorName" #doctorNameField="ngModel">

  <div *ngIf="doctorNameField.invalid && doctorNameField.touched && doctorNameField.dirty">
    Name is required
  </div>

  <button type="submit" [disabled]="!formRef['form'].valid">Save</button>
</form>
```

## Where each part goes

| Token | Meaning |
|---|---|
| `#formRef="ngForm"` | the whole form |
| `name="..."` | required for `ngModel` to register the control |
| `[(ngModel)]` | two-way bind to the component field |
| `#doctorNameField="ngModel"` | that one input’s valid/touched/dirty |
| `required` | empty value → `invalid` |
| `touched` | user focused then left the control |
| `dirty` | user changed the value |
| `readonly` | shown, not edited (doctor id, surgery date, category) |
| `[disabled]="!form.valid"` | block submit while invalid |

PS: message shows only when **invalid and touched and dirty**.

## Messages used

| Screen | Text |
|---|---|
| add / update doctor | `Name is required` |
| update surgery | `Start time is required`, `End time is required` |

Start must be less than end (24-hour numbers already on the model):

```html
<div *ngIf="startTimeField.valid && endTimeField.valid && +startTime >= +endTime">
  Start time must be less than end time
</div>

<button [disabled]="!updateSurgeryForm['form'].valid || +startTime >= +endTime">
```

`+startTime` forces a number compare. `required` does not compare the two times.

## Exam variations

1. `formRef.form.invalid` instead of `['form'].valid` — same form object.
2. Show the message on `invalid && touched` only — fails the “dirty” line in the table.
3. Reactive forms (`FormBuilder`) — not what these templates use.
4. `readonly` is not the same as `disabled`. Disabled controls are omitted from some form posts; these screens use `readonly`.
