# Details #

## The Unit List ##

The unit list is stored in 'Content/units/unitList.xml'

An example list for two units 'soldier' and 'zombie'

```
<UnitList>
   <Unit>soldier</Unit>
   <Unit>zombie</Unit>
</UnitList>
```


## Individual Unit stats ##

For every unit listed in the unit list, there is a corresponding xml file containing the stats of that unit. For the example above, there would be two xml files, 'soldier.xml' and 'zombie.xml'.

An example for soldier (Note: the stats listed here may be out of date. The source for UnitFactory should be looked at as the best resource for the name, type, and order of the attributes.)

```
<Unit>
   <maxHealth>200</maxHealth>
   <speed>0.1</speed>
   <attackRange>2.0</attackRange>
   <attack>10</attack>
       .
       .
       .
</Unit>
```