from time import sleep

dodo = False
sheep = 2

print("Il y a 1 moutons qui a sauté la barrière!")
while not(dodo):
    print("Il y a ", sheep, " moutons qui ont sauté la barrière!")
    sleep(1)
    sheep += 1
